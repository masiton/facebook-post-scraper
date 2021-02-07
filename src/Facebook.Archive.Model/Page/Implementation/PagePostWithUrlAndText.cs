using Facebook.Archive.Model.Page.Architecture;
using Facebook.Archive.Model.Page.Implementation.Base;

namespace Facebook.Archive.Model.Page.Implementation
{
    public class PagePostWithUrlAndText : GenericPagePost, IPageUrlPost, IPageTextPost
    {
        public string LinkUrl { get; set; }

        public string LinkUrlRaw { get; set; }

        public string LinkText { get; set; }

        public string Text { get; set; }

        public string HtmlText { get; set; }

        protected override int GetQualityInternal()
        {
            int quality = 0;

            if(string.IsNullOrWhiteSpace(this.Text) == false)
            {
                quality += 33;
            }

            if(string.IsNullOrWhiteSpace(this.LinkUrl) == false)
            {
                quality += 34;
            }

            if(string.IsNullOrWhiteSpace(this.LinkText) == false)
            {
                quality += 33;
            }

            return quality;
        }
    }
}