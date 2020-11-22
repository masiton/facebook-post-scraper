using HtmlAgilityPack;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading.Tasks;

namespace Facebook.Archive.Runner.Browser
{
    public class FacebookBrowser : IDisposable
    {
        private ChromeDriver current;

        public async Task Initialize()
        {
            ChromeDriver driver = null;

            await Task.Run(() =>
            {
                driver = new ChromeDriver();
            });

            this.current = driver;
        }

        public async Task<HtmlDocument> GetHtmlDocument(string url)
        {
            string html = null;

            await Task.Run(() =>
            {
                this.current.Url = url;
                html = this.current.PageSource;
            });

            if (string.IsNullOrWhiteSpace(html))
            {
                return null;
            }

            var document = new HtmlDocument();
            document.LoadHtml(html);

            return document;
        }

        public void Dispose()
        {
            this.current.Quit();
        }
    }
}