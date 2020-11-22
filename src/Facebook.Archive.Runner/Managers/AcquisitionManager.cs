using Facebook.Archive.Data.Model;
using Facebook.Archive.Data.Persistence;
using Facebook.Archive.Runner.Browser;
using Facebook.Archive.Runner.Handlers;
using Facebook.Archive.Runner.Parsers.Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Facebook.Archive.Runner.Managers
{
    public class AcquisitionManager
    {
        private readonly FacebookUrlHandler facebookUrlHandler;
        private readonly UnitOfWorkScopeProvider unitOfWorkScopeProvider;
        private readonly FacebookBrowserProvider facebookBrowserProvider;
        private readonly FacebookPagePostParser facebookPagePostParser;

        public AcquisitionManager(
            FacebookUrlHandler facebookUrlHandler,
            UnitOfWorkScopeProvider unitOfWorkScopeProvider,
            FacebookBrowserProvider facebookBrowserProvider,
            FacebookPagePostParser facebookPagePostParser)
        {
            this.facebookUrlHandler = facebookUrlHandler;
            this.unitOfWorkScopeProvider = unitOfWorkScopeProvider;
            this.facebookBrowserProvider = facebookBrowserProvider;
            this.facebookPagePostParser = facebookPagePostParser;
        }

        public async Task Run()
        {
            var targets = new List<Page>();
            var dbUpdate = (Update)null;

            using(var scope = this.unitOfWorkScopeProvider.GetScope())
            {
                dbUpdate = new Update
                {
                    StartUtc = DateTime.UtcNow
                };

                scope.UnitOfWork.Updates.Add(dbUpdate);
                scope.SaveChanges();

                targets.AddRange(scope.UnitOfWork.Pages.AsNoTracking().ToList());
            }

            using(var browser = await this.facebookBrowserProvider.GetFacebookBrowser())
            {
                foreach(var target in targets)
                {
                    var postsUrl = this.facebookUrlHandler.GetPostsUrlForPage(target.Url);
                    var htmlDocument = await browser.GetHtmlDocument(postsUrl);
                    var posts = this.facebookPagePostParser.GetFacebookPosts(htmlDocument);

                    foreach (var post in posts)
                    {
                        using (var scope = this.unitOfWorkScopeProvider.GetScope())
                        {
                            var dbPost = scope.UnitOfWork.Posts.AsTracking().SingleOrDefault(x => x.Url == post.Url);

                            if(dbPost == null)
                            {
                                dbPost = new Post
                                { 
                                    Url = post.Url
                                };

                                scope.UnitOfWork.Posts.Add(dbPost);
                                scope.SaveChanges();
                            }

                            var postUpdate = new PostUpdate
                            {
                                Post = dbPost,
                                Update = dbUpdate,
                                Timestamp = DateTime.UtcNow
                            };

                            var postContent = new PostContent
                            {
                                PostUpdate = postUpdate,
                                RawHtml = post.Html,
                                Text = post.Text
                            };

                            scope.UnitOfWork.PostUpdates.Add(postUpdate);
                            scope.UnitOfWork.PostContents.Add(postContent);

                            dbUpdate.EndUtc = DateTime.UtcNow;
                            dbUpdate.IsSuccessful = true;

                            scope.SaveChanges();
                        }
                    }
                }
            }
        }
    }
}
