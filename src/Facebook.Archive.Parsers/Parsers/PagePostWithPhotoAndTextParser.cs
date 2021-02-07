using Facebook.Archive.Model.Page.Architecture.Base;
using Facebook.Archive.Model.Page.Implementation;
using Facebook.Archive.Parsers.Parsers.Base;
using HtmlAgilityPack;
using System.Net.Http;
using System.Threading.Tasks;

namespace Facebook.Archive.Parsers.Parsers
{
    public class PagePostWithPhotoAndTextParser : GenericParser, IParser<PagePostWithPhotoAndText>
    {
        public string ParserName => nameof(PagePostWithPhotoAndTextParser);

        public int ParserVersion => 1;

        public PagePostWithPhotoAndText Parse(HtmlNode node)
        {
            var post = new PagePostWithPhotoAndText
            {
                Html = node.InnerHtml,
                Parser = this.ParserName,
                ParserVersion = this.ParserVersion,
            };

            var nameNode = this.GetNameHtmlNode(node);
            var timestampNode = this.GetTimestampNodeFromNameNode(nameNode);

            post.Url = this.GetPostUrlFromTimestampNode(timestampNode);
            post.TimestampRaw = this.GetTimestampStringFromTimestampNode(timestampNode);
            post.Timestamp = this.GetDateFromPostString(post.TimestampRaw);

            var postMessageNode = node.SelectSingleNode(".//div[contains(@data-testid, 'post_message')]");

            if (postMessageNode != null)
            {
                post.Text = postMessageNode.SelectSingleNode(".//p").InnerText;
                post.HtmlText = postMessageNode.InnerHtml;
            }

            var contentCollectionNode = nameNode.ParentNode.ParentNode.ParentNode.ParentNode.ParentNode.ParentNode.ParentNode.ParentNode;

            if (contentCollectionNode.ChildNodes.Count == 4)
            {
                var mediaContentCandidateNode = contentCollectionNode.ChildNodes[2];
                var imageContentCandidateNode = mediaContentCandidateNode.SelectSingleNode(".//img[contains(@class, 'scaledImageFitWidth img')]");

                if (imageContentCandidateNode?.Attributes.Contains("src") == true)
                {
                    post.ImageUrl = this.EscapeUrlSequence(imageContentCandidateNode.Attributes["src"].Value);
                    post.ImageData = this.GetImageBytes(post.ImageUrl);
                }
            }

            return post;
        }
    }
}