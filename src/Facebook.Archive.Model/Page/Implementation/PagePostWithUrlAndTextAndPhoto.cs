using Facebook.Archive.Model.Page.Architecture;
using Facebook.Archive.Model.Page.Implementation.Base;

namespace Facebook.Archive.Model.Page.Implementation
{
    public class PagePostWithUrlAndTextAndPhoto : GenericPagePost, IPageUrlPost, IPageTextPost, IPagePhotoPost
    {
        public string LinkUrl { get; set; }

        public string LinkUrlRaw { get; set; }

        public string LinkText { get; set; }

        public string Text { get; set; }

        public string HtmlText { get; set; }

        public string ImageUrl { get; set; }

        public byte[] ImageData { get; set; }

        protected override int GetQualityInternal()
        {
            int quality = 0;

            if(string.IsNullOrWhiteSpace(this.Text) == false)
            {
                quality += 20;
            }

            if(string.IsNullOrWhiteSpace(this.LinkUrl) == false)
            {
                quality += 20;
            }

            if(string.IsNullOrWhiteSpace(this.LinkText) == false)
            {
                quality += 20;
            }

            if (string.IsNullOrWhiteSpace(this.ImageUrl) == false)
            {
                quality += 20;
            }

            if (this.ImageData?.Length > 0)
            {
                quality += 20;
            }

            return quality;
        }
    }
}