using Facebook.Archive.Model.Page.Implementation;
using Facebook.Archive.Parsers.Parsers.Base;
using HtmlAgilityPack;

namespace Facebook.Archive.Parsers.Parsers
{
    public class PagePostWithTextParser : GenericParser, IParser<PagePostWithText>
    {
        public string ParserName => nameof(PagePostWithTextParser);

        public int ParserVersion => 1;

        public PagePostWithText Parse(HtmlNode node)
        {
            var post = new PagePostWithText
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

            return post;
        }
    }
}