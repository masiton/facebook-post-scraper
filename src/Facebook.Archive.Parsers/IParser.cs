using Facebook.Archive.Model.Page.Architecture.Base;
using HtmlAgilityPack;

namespace Facebook.Archive.Parsers
{
    public interface IParser
    {
        string ParserName { get; }

        int ParserVersion { get; }
    }

    public interface IParser<out T> : IParser where T : class, IElement
    {
        T Parse(HtmlNode node);
    }
}