using Facebook.Archive.Model.Page;
using Facebook.Archive.Model.Page.Parts;
using HtmlAgilityPack;
using System;
using System.Globalization;
using System.Linq;
using System.Net.Http;

namespace Facebook.Archive.Parsers
{
    public class FacebookPagePostParser : IParser<Post>
    {
        public string ParserName => nameof(FacebookPagePostParser);

        public int ParserVersion => 1;

        public Post Parse(HtmlNode node)
        {
            var post = new Post()
            {
                Html = node.InnerHtml,
                ParserName = this.ParserName,
                ParserVersion = this.ParserVersion
            };

            var nameNode = this.GetNameHtmlNode(node);
            var timestampNode = this.GetTimestampNodeFromNameNode(nameNode);

            post.Url = this.GetPostUrlFromTimestampNode(timestampNode);
            post.TimestampRaw = this.GetTimestampStringFromTimestampNode(timestampNode);
            post.Timestamp = this.GetDateFromPostString(post.TimestampRaw);

            var postMessageNode = node.SelectSingleNode(".//div[contains(@data-testid, 'post_message')]");

            if (postMessageNode != null)
            {
                post.Texts.Add(new Model.Page.Parts.PostText
                {
                    Kind = Model.Page.Parts.Enums.PostTextKind.Original,
                    Text = postMessageNode.SelectSingleNode(".//p").InnerText,
                    TextHtml = postMessageNode.InnerHtml
                });
            }

            var contentCollectionNode = nameNode.ParentNode.ParentNode.ParentNode.ParentNode.ParentNode.ParentNode.ParentNode.ParentNode;

            if (contentCollectionNode.ChildNodes.Count == 4)
            {
                var mediaContentCandidateNode = contentCollectionNode.ChildNodes[2];
                var imageContentCandidateNode = mediaContentCandidateNode.SelectSingleNode(".//img[contains(@class, 'scaledImageFitWidth img')]");

                if (imageContentCandidateNode?.Attributes.Contains("src") == true)
                {
                    var image = new PostImage
                    {
                        ImageUrlHtml = imageContentCandidateNode.Attributes["src"].Value,
                        IsVideo = false
                    };

                    image.ImageUrl = this.EscapeUrlSequence(image.ImageUrlHtml);
                    image.ImageData = this.GetImageBytes(image.ImageUrl);

                    post.Images.Add(image);

                    // see if there is more pics
                    if(imageContentCandidateNode.ParentNode?.ParentNode?.ParentNode?.ChildNodes.Count > 1 && imageContentCandidateNode.ParentNode?.ParentNode?.ParentNode?.ChildNodes.All(x => x.Name == "a") == true)
                    {
                        var additionalANodes = imageContentCandidateNode.ParentNode?.ParentNode?.ParentNode?.ChildNodes.Skip(1).ToList();

                        foreach(var aNode in additionalANodes)
                        {
                            var imgNode = aNode.SelectSingleNode(".//img[contains(@class, 'scaledImageFitWidth img')] | .//img[contains(@class, 'scaledImageFitHeight img')]");

                            if(imgNode == null)

                            if(imgNode == null)
                            {
                                continue;
                            }

                            var img = new PostImage
                            {
                                ImageUrlHtml = imgNode.Attributes["src"].Value,
                                IsVideo = false
                            };

                            img.ImageUrl = this.EscapeUrlSequence(img.ImageUrlHtml);
                            img.ImageData = this.GetImageBytes(img.ImageUrl);
                            
                            post.Images.Add(img);
                        }
                    }
                }

                var sharedPostUrl = mediaContentCandidateNode.SelectSingleNode(".//div[contains(@data-testid, 'post_message')]")
                    ?.ParentNode
                    ?.SelectSingleNode(".//span[contains(@class, 'timestampContent')]")
                    ?.ParentNode
                    ?.ParentNode.Attributes["href"]?.Value;

                if (string.IsNullOrWhiteSpace(sharedPostUrl) == false)
                {
                    post.SharedPostUrl = $"https://www.facebook.com{sharedPostUrl}";
                }

                var linkSpanNode = mediaContentCandidateNode.SelectSingleNode(".//span");
                var linkANode = linkSpanNode?.ChildNodes.Count > 1
                    ? linkSpanNode.ChildNodes[1].SelectSingleNode(".//a[@aria-label]")
                    : null;

                if(linkANode == null && linkSpanNode?.ChildNodes.Count == 1)
                {
                    linkANode = linkSpanNode?.SelectSingleNode(".//a[@aria-label]");
                }

                if (linkANode != null)
                {
                    var link = new PostLink()
                    {
                        LinkUrlHtml = linkANode.Attributes["href"].Value,
                        LinkText = linkANode.Attributes["aria-label"].Value
                    };

                    link.LinkUrl = this.GetLinkUrlFromFacebookLink(link.LinkUrlHtml);
                    post.Links.Add(link);
                }
            }

            return post;
        }

        private byte[] GetImageBytes(string url)
        {
            using (var httpClient = new HttpClient())
            {
                var task = httpClient.GetByteArrayAsync(url);
                task.Wait();

                if (task.Status == System.Threading.Tasks.TaskStatus.RanToCompletion)
                {
                    return task.Result;
                }

                throw new InvalidOperationException();
            }
        }

        private HtmlNode GetNameHtmlNode(HtmlNode postNode)
        {
            return postNode.SelectSingleNode(".//h5");
        }

        private HtmlNode GetTimestampNodeFromNameNode(HtmlNode nameNode)
        {
            return nameNode.ParentNode.SelectSingleNode(".//div").SelectSingleNode(".//a");
        }

        private string GetPostUrlFromTimestampNode(HtmlNode timestampNode)
        {
            var value = timestampNode.Attributes["href"].Value.Substring(0, timestampNode.Attributes["href"].Value.IndexOf('?'));
            return $"https://www.facebook.com{value}";
        }

        private string GetTimestampStringFromTimestampNode(HtmlNode timestampNode)
        {
            return timestampNode.SelectSingleNode(".//span[contains(@class, 'timestampContent')]").ParentNode.Attributes["title"].Value;
        }

        private DateTime GetDateFromPostString(string value)
        {
            var values = value.Trim().Split(new[] { " at" }, StringSplitOptions.RemoveEmptyEntries);
            var formatted = string.Join(null, values);
            var date = DateTime.ParseExact(formatted, "dddd, MMMM d, yyyy h:mm tt", CultureInfo.InvariantCulture);
            return date;
        }

        private string EscapeUrlSequence(string url)
        {
            return url.Replace("&amp;", "&").Replace("%3A", ":").Replace("%2F", "/");
        }

        private string GetLinkUrlFromFacebookLink(string facebookUrl)
        {
            var value = facebookUrl.Substring("https://l.facebook.com/l.php?u=".Length);
            value = System.Net.WebUtility.UrlDecode(value);

            return value;
        }
    }
}
