using System.Threading.Tasks;

namespace Facebook.Archive.Runner.Browser
{
    public class FacebookBrowserProvider
    {
        public async Task<FacebookBrowser> GetFacebookBrowser()
        {
            var browser = new FacebookBrowser();
            await browser.Initialize();

            return browser;
        }
    }
}
