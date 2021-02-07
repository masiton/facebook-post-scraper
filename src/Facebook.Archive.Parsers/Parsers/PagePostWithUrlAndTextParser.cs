using Facebook.Archive.Model.Page.Implementation;
using Facebook.Archive.Parsers.Parsers.Base;
using HtmlAgilityPack;

namespace Facebook.Archive.Parsers.Parsers
{
    public class PagePostWithUrlAndTextParser : GenericParser, IParser<PagePostWithUrlAndText>
    {
        public string ParserName => nameof(PagePostWithUrlParser);

        public int ParserVersion => 1;

        public PagePostWithUrlAndText Parse(HtmlNode node)
        {
            var post = new PagePostWithUrlAndText
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