using Facebook.Archive.Model.Page.Architecture.Base;

namespace Facebook.Archive.Model.Page.Architecture
{
    public interface IPageTextPost : IPagePost
    {
        string Text { get; }

        /// <summary>
        /// Includes emojis and such.
        /// </summary>
        string HtmlText { get; }
    }
}