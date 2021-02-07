using Facebook.Archive.Data.Model;
using Facebook.Archive.Data.Persistence;
using Facebook.Archive.Model.Page.Architecture;
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
                    var posts = await this.facebookPagePostParser.GetFacebookPosts(htmlDocument, browser);

                    foreach (var post in posts)
                    {
                        using (var scope = this.unitOfWorkScopeProvider.GetScope())
                        {
                            var dbPost = scope.UnitOfWork.Posts.AsTracking().SingleOrDefault(x => x.Url == post.Url);

                            if (dbPost == null)
                            {
                                dbPost = new Post
                                {
                                    Url = post.Url,
                                };

                                scope.UnitOfWork.Posts.Add(dbPost);
                            }

                            var dbPostUpdate = new PostUpdate
                            {
                                Post = dbPost,
                                Update = dbUpdate,
                                Timestamp = DateTime.UtcNow
                            };

                            scope.UnitOfWork.PostUpdates.Add(dbPostUpdate);

                            var dbPostContent = new PostContent
                            {
                                PostUpdate = dbPostUpdate,
                                Html = post.Html,
                                ParserName = post.Parser,
                                ParserVersion = post.ParserVersion,
                            };

                            scope.UnitOfWork.PostContents.Add(dbPostContent);

                            if(post.Timestamp.HasValue)
                            {
                                var dbPostContentTimestamp = new PostContentTimestamp
                                {
                                    TimestampUtc = post.Timestamp.Value,
                                    TimestampRaw = post.TimestampRaw,
                                    PostContent = dbPostContent
                                };

                                scope.UnitOfWork.PostContentTimestamps.Add(dbPostContentTimestamp);
                            }

                            if(post is IPageTextPost tp)
                            {
                                var dbPostContentText = new PostContentText
                                {
                                    Html = tp.Html,
                                    Text = tp.Text,
                                    PostContent = dbPostContent
                                };

                                scope.UnitOfWork.PostContentTexts.Add(dbPostContentText);
                            }

                            if(post is IPagePhotoPost pp)
                            {
                                var dbPostContentPhoto = new PostContentPhoto
                                {
                                    ImageData = pp.ImageData,
                                    ImageUrl = pp.ImageUrl,
                                    PostContent = dbPostContent
                                };

                                scope.UnitOfWork.PostContentPhotos.Add(dbPostContentPhoto);
                            }

                            if(post is IPageUrlPost up)
                            {
                                var dbPostContentUrl = new PostContentUrl
                                {
                                    Text = up.LinkText,
                                    Url = up.LinkUrl,
                                    PostContent = dbPostContent
                                };

                                scope.UnitOfWork.PostContentUrls.Add(dbPostContentUrl);
                            }

                            scope.SaveChanges();
                        }
                    }
                }
            }
        }
    }
}