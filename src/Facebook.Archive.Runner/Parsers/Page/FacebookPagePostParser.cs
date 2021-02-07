using Facebook.Archive.Model.Page.Architecture.Base;
using Facebook.Archive.Parsers;
using Facebook.Archive.Parsers.Parsers;
using Facebook.Archive.Runner.Browser;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Facebook.Archive.Runner.Parsers.Page
{
    public class FacebookPagePostParser
    {
        private List<IParser<IPagePost>> parsers = new List<IParser<IPagePost>>
        {
            new PagePostWithUrlAndTextAndPhotoParser(),
            new PagePostWithUrlAndTextParser(),
            new PagePostWithUrlAndPhotoParser(),
            new PagePostWithUrlParser(),
            new PagePostWithPhotoAndTextParser(),
            new PagePostWithTextParser(),
            new PagePostWithPhotoParser(),
        };

        public async Task<List<IPagePost>> GetFacebookPosts(HtmlDocument document, FacebookBrowser browser)
        {
            var acceptAllButton = document.DocumentNode.SelectSingleNode(".//button[contains(@title, 'Accept All')]");

            if(acceptAllButton != null)
            {
                await browser.ClickButton(acceptAllButton.XPath);
            }

            var postElements = this.GetPostHtmlNodes(document);
            var posts = new List<IPagePost>();

            foreach(var postElement in postElements)
            {
                foreach (var parser in this.parsers)
                {
                    try
                    {
                        var parsed = parser.Parse(postElement);

                        if (parsed.GetQuality() >= 99)
                        {
                            posts.Add(parsed);
                            break;
                        }
                    }
                    catch
                    {
                        continue;
                    }
                }
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
