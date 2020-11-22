using Facebook.Archive.Runner.Model.Facebook.Page;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Facebook.Archive.Runner.Parsers.Page
{
    public class FacebookPagePostParser
    {
        public List<Post> GetFacebookPosts(HtmlDocument document)
        {
            var postElements = this.GetPostHtmlNodes(document);
            var posts = postElements.Select(x => this.GetPostFromHtmlNode(x)).ToList();

            return posts;
        }

        private Post GetPostFromHtmlNode(HtmlNode node)
        {
            var nameParagraph = node.SelectSingleNode(".//h5");
            var dateHyperlink = nameParagraph.ParentNode.SelectSingleNode(".//div").SelectSingleNode(".//a");
            var url = dateHyperlink.Attributes["href"].Value;
            url = url.Substring(0, url.IndexOf('?'));
            var date = GetDateFromPostString(dateHyperlink.SelectSingleNode(".//span[contains(@class, 'timestampContent')]").ParentNode.Attributes["title"].Value);

            var text = string.Empty;

            if(node.SelectSingleNode(".//div[contains(@data-testid, 'post_message')]") != null)
            {
                text = node.SelectSingleNode(".//div[contains(@data-testid, 'post_message')]").SelectSingleNode(".//p").InnerText;
            }

            var imageNode = node.SelectSingleNode(".//img");

            if (imageNode != null)
            {
                var imgSrc = imageNode.Attributes["src"].Value;
            }

            return new Post
            {
                Html = node.InnerHtml,
                Url = url,
                Text = text,
                TimestampUtc = date
            };
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

        private DateTime GetDateFromPostString(string value)
        {
            var values = value.Trim().Split(" at");
            var formatted = string.Join(null, values);
            var date = DateTime.ParseExact(formatted, "dddd, MMMM d, yyyy h:mm tt", CultureInfo.InvariantCulture);
            return date;
        }
    }
}
