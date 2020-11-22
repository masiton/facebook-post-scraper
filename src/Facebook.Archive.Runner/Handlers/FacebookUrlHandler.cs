namespace Facebook.Archive.Runner.Handlers
{
    public class FacebookUrlHandler
    {
        public string GetPostsUrlForPage(string pageUrl)
        {
            var pageName = pageUrl.Substring(Constants.FacebookUrl.Length + 1);
            return string.Format(Constants.FacebookPagesUrlMask, pageName);
        }
    }
}
