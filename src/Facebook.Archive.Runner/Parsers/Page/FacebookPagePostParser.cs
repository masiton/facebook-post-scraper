using Facebook.Archive.Runner.Browser;
using Facebook.Archive.Runner.Model.Facebook.Page;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Facebook.Archive.Runner.Parsers.Page
{
    public class FacebookPagePostParser
    {
        public async Task<List<Post>> GetFacebookPosts(HtmlDocument document, FacebookBrowser browser)
        {
            var acceptAllButton = document.DocumentNode.SelectSingleNode(".//button[contains(@title, 'Accept All')]");

            if(acceptAllButton != null)
            {
                await browser.ClickButton(acceptAllButton.XPath);
            }

            var postElements = this.GetPostHtmlNodes(document);
            var posts = new List<Post>();

            foreach(var postElement in postElements)
            {
                posts.Add(await this.GetPostFromHtmlNode(postElement, browser));
            }

            return posts;
        }

        private async Task<Post> GetPostFromHtmlNode(HtmlNode node, FacebookBrowser browser)
        {
            var nameParagraph = node.SelectSingleNode(".//h5");
            var dateHyperlink = nameParagraph.ParentNode.SelectSingleNode(".//div").SelectSingleNode(".//a");
            var url = dateHyperlink.Attributes["href"].Value;
            url = url.Substring(0, url.IndexOf('?'));
            var date = GetDateFromPostString(dateHyperlink.SelectSingleNode(".//span[contains(@class, 'timestampContent')]").ParentNode.Attributes["title"].Value);

            string text = null;

            byte[] attachment = null;
            string attachmentTitle = null;
            string link = null;

            var postMessageNode = node.SelectSingleNode(".//div[contains(@data-testid, 'post_message')]");

            if (postMessageNode != null)
            {
                text = postMessageNode.SelectSingleNode(".//p").InnerText;

                var postMessageNodeIndex = postMessageNode.ParentNode.ChildNodes.IndexOf(postMessageNode);
                
                if (postMessageNode.ParentNode.ChildNodes.Count > postMessageNodeIndex + 1)
                {
                    var mediaContentCandidateNode = postMessageNode.ParentNode.ChildNodes[postMessageNodeIndex + 1];
                    var imageContentCandidateNode = mediaContentCandidateNode.SelectSingleNode(".//img[contains(@class, 'scaledImageFitWidth img')]");

                    if (imageContentCandidateNode?.Attributes.Contains("src") == true)
                    {
                        var mediaUrl = this.EscapeUrlSequence(imageContentCandidateNode.Attributes["src"].Value);

                        using (var httpClient = new HttpClient())
                        {
                            attachment = await httpClient.GetByteArrayAsync(mediaUrl);
                        }
                    }

                    var targetNode = mediaContentCandidateNode.SelectSingleNode(".//span");

                    if (targetNode?.ChildNodes.Count > 1)
                    {
                        var aNode = targetNode.ChildNodes[1].SelectSingleNode(".//a[@aria-label]");
                        if (aNode != null)
                        {
                            attachmentTitle = aNode.Attributes["aria-label"].Value;
                            link = aNode.Attributes["href"].Value;
                        }
                    }
                }
            }
            else
            {
                var contentCollectionNode = nameParagraph.ParentNode.ParentNode.ParentNode.ParentNode.ParentNode.ParentNode.ParentNode.ParentNode;
                if(contentCollectionNode.ChildNodes.Count == 4)
                {
                    var mediaContentCandidateNode = contentCollectionNode.ChildNodes[2];
                    var imageContentCandidateNode = mediaContentCandidateNode.SelectSingleNode(".//img[contains(@class, 'scaledImageFitWidth img')]");

                    if (imageContentCandidateNode?.Attributes.Contains("src") == true)
                    {
                        var mediaUrl = this.EscapeUrlSequence(imageContentCandidateNode.Attributes["src"].Value);

                        using (var httpClient = new HttpClient())
                        {
                            attachment = await httpClient.GetByteArrayAsync(mediaUrl);
                        }
                    }

                    var targetNode = mediaContentCandidateNode.SelectSingleNode(".//span");

                    if (targetNode?.ChildNodes.Count > 1)
                    {
                        var aNode = targetNode.ChildNodes[1].SelectSingleNode(".//a[@aria-label]");
                        attachmentTitle = aNode.Attributes["aria-label"].Value;
                        link = aNode.Attributes["href"].Value;
                    }
                }
            }

            return new Post
            {
                Html = node.InnerHtml,
                Url = url,
                Text = text,
                TimestampUtc = date,
                AttachmentData = attachment,
                AttachmentDescription = attachmentTitle,
                AttachmentLink = link
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

        private string EscapeUrlSequence(string url)
        {
            return url.Replace("&amp;", "&").Replace("%3A", ":").Replace("%2F", "/");
        }
    }
}
