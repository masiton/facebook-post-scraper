using Facebook.Archive.Model.Page.Architecture;
using Facebook.Archive.Model.Page.Implementation.Base;

namespace Facebook.Archive.Model.Page.Implementation
{
    public class PagePostWithText : GenericPagePost, IPageTextPost
    {
        public string Text { get; set; }

        public string HtmlText { get; set; }

        protected override int GetQualityInternal()
        {
            int quality = 0;

            if (string.IsNullOrWhiteSpace(this.Text) == false)
            {
                quality += 50;
            }

            if(string.IsNullOrWhiteSpace(this.HtmlText) == false)
            {
                quality += 50;
            }

            return quality;
        }
    }
}