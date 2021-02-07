using Facebook.Archive.Model.Page.Implementation;
using Facebook.Archive.Parsers.Parsers.Base;
using HtmlAgilityPack;
using System.Net.Http;

namespace Facebook.Archive.Parsers.Parsers
{
    public class PagePostWithPhotoParser : GenericParser, IParser<PagePostWithPhoto>
    {
        public string ParserName => nameof(PagePostWithPhotoParser);

        public int ParserVersion => 1;

        public PagePostWithPhoto Parse(HtmlNode node)
        {
            var post = new PagePostWithPhoto
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
            }

            return post;
        }
    }
}