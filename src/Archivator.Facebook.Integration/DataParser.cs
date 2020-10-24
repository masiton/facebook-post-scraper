using AngleSharp;
using Archivator.Facebook.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archivator.Facebook.Integration
{
    public class DataParser
    {
        private static readonly string[] trashElementNames = { "script", "style", "meta", "link" };
        private static readonly string[] sanitationElementNames = { "div", "p", "span", "td", "table", "a", "i", "noscript", "h1", "h2", "h3" };
        private static readonly string[] trashAttributeNames = { "class", "id", "href", "data-ft", "data-fte", "data-gt", "style", "target", "aria-hidden", "tabindex", "data-hover", "data-tooltip-content", "data-tooltip-alignh", "data-visualcompletion", "data-ftr", "role" };

        public static async Task<List<Post>> GetPostsFromUri(string uri)
        {
            var config = Configuration.Default.WithDefaultLoader();
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(uri);

            // remove elements of no value.
            document
                .QuerySelectorAll(string.Join(", ", trashElementNames))
                .ToList()
                .ForEach(RemoveElement);

            // remove posts added by other people
            FindElementsWithAttributeAndAttributeValue(document, "div", "data-referrer", "PagePostsByOthersPagelet")
                .ForEach(RemoveElement);

            // remove navigation menubar
            FindElementsWithAttributeAndAttributeValue(document, "div", "data-testid", "ax-navigation-menubar")
                .ForEach(RemoveElement);

            // remove bluebar
            FindElementsWithAttributeAndAttributeValue(document, "div", "data-referrer", "pagelet_bluebar")
                .ForEach(RemoveElement);

            // sanitize elements by removing attributes of no value.
            var sanitationElements = document.QuerySelectorAll(string.Join(", ", sanitationElementNames));

            foreach (var element in sanitationElements)
            {
                foreach (var trashAttributeName in trashAttributeNames)
                {
                    if (element.HasAttribute(trashAttributeName))
                    {
                        element.RemoveAttribute(trashAttributeName);
                    }
                }
            }

            var postElements = document.QuerySelectorAll("div").Where(x => x.HasAttribute("data-xt")).ToList();
            var posts = new List<Post>();

            foreach(var postElement in postElements)
            {
                // get creation date
                var subtitle = postElement.QuerySelectorAll("div").First(x => x.HasAttribute("data-testid") && x.GetAttribute("data-testid").Equals("story-subtitle"));
                var unixEpoch = Convert.ToInt32(subtitle.QuerySelectorAll("abbr").Single(x => x.HasAttribute("data-utime")).GetAttribute("data-utime"));
                var creationDateUtc = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(unixEpoch);

                // get post text
                var postMessage = postElement.QuerySelectorAll("div").Where(x => x.HasAttribute("data-testid") && x.GetAttribute("data-testid") == "post_message").FirstOrDefault();
                string contents = string.Empty;

                if (postMessage == null)
                {
                    // image post
                    var imgs = postElement.QuerySelectorAll("img").Where(x => x.HasAttribute("alt"));

                    if (imgs.Any())
                    {
                        contents = imgs.OrderByDescending(x => x.GetAttribute("alt").Length).First().GetAttribute("alt");
                    }
                }
                else
                {
                    contents = postMessage.TextContent;
                }

                posts.Add(new Post() { CreatedUtc = creationDateUtc, Text = contents });
            }

            return posts;
        }

        private static void RemoveElement(AngleSharp.Dom.IElement element)
        {
            var parent = element.Parent;
            parent.RemoveChild(element);
        }

        private static List<AngleSharp.Dom.IElement> FindElementsWithAttributeAndAttributeValue(AngleSharp.Dom.IDocument document, string elementName, string attributeName, string attributeValueStartsWith)
        {
            return document
                .QuerySelectorAll(elementName)
                .Where(x => x.HasAttribute(attributeName) && x.GetAttribute(attributeName).StartsWith(attributeValueStartsWith))
                .ToList();
        }
    }
}
