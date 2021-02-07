using Facebook.Archive.Model.Page.Parts.Enums;

namespace Facebook.Archive.Model.Page.Parts
{
    public class PostText
    {
        public PostTextKind Kind { get; set; }

        public string Text { get; set; }

        /// <summary>
        /// Includes emojis and such.
        /// </summary>
        public string TextHtml { get; set; }
    }
}