using Facebook.Archive.Model.Page;
using Facebook.Archive.Parsers;
using Facebook.Archive.Runner.Browser;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Facebook.Archive.Runner.Parsers.Page
{
    public class FacebookPageParser
    {
        private FacebookPagePostParser pagePostParser = new FacebookPagePostParser();

        public async Task<List<Post>> GetFacebookPosts(HtmlDocument document, FacebookBrowser browser)
        {
            var acceptAllButton = document.DocumentNode.SelectSingleNode(".//button[contains(@title, 'Accept All')]");

            if(acceptAllButton != null)
            {
                await browser.ClickButton(acceptAllButton.XPath);
            }

            var postElements = this.GetPostHtmlNodes(document);
            var posts = new List<Post>();

            foreach (var postElement in postElements)
            {
                var parsed = pagePostParser.Parse(postElement);
                posts.Add(parsed);
            }

            return posts;
        }

        private List<HtmlNode> GetPostHtmlNodes(HtmlDocument postPage)
        {
            var pagelet = postPage.DocumentNode.SelectSingleNode("//div[contains(@id, 'pagelet_timeline_main_column')]");
            var div = pagelet.ChildNodes[0].ChildNodes[1].ChildNodes[0];
            var posts = div.ChildNodes.Where(x => x.Attributes.Count == 1).ToList();
            var postNodes = posts.Where(x => x.Attributes.Contains("class")).ToList();

            if (postNodes.Count > 0)
            {
                var firstPostClassValue = postNodes.First().Attributes["class"].Value;
                postNodes = postNodes.Where(x => x.Attributes["class"].Value == firstPostClassValue).ToList();
            }

            return postNodes;
        }
    }
}
