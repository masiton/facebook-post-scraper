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
        private readonly FacebookPageParser facebookPagePostParser;

        public AcquisitionManager(
            FacebookUrlHandler facebookUrlHandler,
            UnitOfWorkScopeProvider unitOfWorkScopeProvider,
            FacebookBrowserProvider facebookBrowserProvider,
            FacebookPageParser facebookPagePostParser)
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
                    Console.WriteLine(target.Url);

                    var postsUrl = this.facebookUrlHandler.GetPostsUrlForPage(target.Url);
                    var htmlDocument = await browser.GetHtmlDocument(postsUrl);
                    var posts = (List<Facebook.Archive.Model.Page.Post>)null;

                    try
                    {
                        posts = await this.facebookPagePostParser.GetFacebookPosts(htmlDocument, browser);
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        continue;
                    }

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
                                ParserName = post.ParserName,
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

                            if (post.Texts.Any() == true)
                            {
                                post.Texts.ForEach(x => scope.UnitOfWork.PostContentTexts.Add(new PostContentText
                                {
                                    PostContent = dbPostContent,
                                    Text = x.Text,
                                    Html = x.TextHtml
                                }));
                            }

                            if(post.Images.Any() == true)
                            {
                                post.Images.ForEach(x => scope.UnitOfWork.PostContentImages.Add(new PostContentImage
                                {
                                    PostContent = dbPostContent,
                                    ImageData = x.ImageData,
                                    ImageUrl = x.ImageUrl,
                                    ImageUrlHtml = x.ImageUrlHtml
                                }));
                            }

                            if(post.Links.Any() == true)
                            {
                                post.Links.ForEach(x => scope.UnitOfWork.PostContentLinks.Add(new PostContentLink
                                {
                                    PostContent = dbPostContent,
                                    Text = x.LinkText,
                                    Url = x.LinkUrl,
                                    UrlHtml = x.LinkUrlHtml
                                }));
                            }

                            scope.SaveChanges();
                        }
                    }
                }
            }
        }
    }
}