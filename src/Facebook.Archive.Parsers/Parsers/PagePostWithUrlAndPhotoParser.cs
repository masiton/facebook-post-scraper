using Facebook.Archive.Model.Page.Implementation;
using Facebook.Archive.Parsers.Parsers.Base;
using HtmlAgilityPack;
using System.Net.Http;
using System.Threading.Tasks;

namespace Facebook.Archive.Parsers.Parsers
{
    public class PagePostWithUrlAndPhotoParser : GenericParser, IParser<PagePostWithUrlAndPhoto>
    {
        public string ParserName => nameof(PagePostWithUrlParser);

        public int ParserVersion => 1;

        public PagePostWithUrlAndPhoto Parse(HtmlNode node)
        {
            var post = new PagePostWithUrlAndPhoto
            {
                Html = node.InnerHtml,
                Parser = this.ParserName,
                ParserVersion = this.ParserVersion
            };

            var nameNode = this.GetNameHtmlNode(node);
            var timestampNode = this.GetTimestampNodeFromNameNode(nameNode);

            post.Url = this.GetPostUrlFromTimestampNode(timestampNode);
            post.TimestampRaw = this.GetTimestampStringFromTimestampNode(timestampNode);
            post.Timestamp = this.GetDateFromPostString(post.TimestampRaw);

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

                var targetNode = mediaContentCandidateNode.SelectSingleNode(".//span");

                if (targetNode?.ChildNodes.Count > 1)
                {
                    var aNode = targetNode.ChildNodes[1].SelectSingleNode(".//a[@aria-label]");
                    post.LinkText =  aNode.Attributes["aria-label"].Value;
                    post.LinkUrlRaw = aNode.Attributes["href"].Value;
                    post.LinkUrl = this.GetLinkUrlFromFacebookLink(post.LinkUrlRaw);
                }
            }

            return post;
        }
    }
}