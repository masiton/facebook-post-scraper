using Facebook.Archive.Model.Base;
using HtmlAgilityPack;

namespace Facebook.Archive.Parsers
{
    public interface IParser
    {
        string ParserName { get; }

        int ParserVersion { get; }
    }

    public interface IParser<out T> : IParser where T : FacebookElement
    {
        T Parse(HtmlNode node);
    }
}