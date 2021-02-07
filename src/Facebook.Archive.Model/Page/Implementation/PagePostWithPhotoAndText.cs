using Facebook.Archive.Model.Page.Architecture;
using Facebook.Archive.Model.Page.Implementation.Base;

namespace Facebook.Archive.Model.Page.Implementation
{
    public class PagePostWithPhotoAndText : GenericPagePost, IPageTextPost, IPagePhotoPost
    {
        public string ImageUrl { get; set; }

        public byte[] ImageData { get; set; }

        public string Text { get; set; }

        public string HtmlText { get; set; }

        protected override int GetQualityInternal()
        {
            var quality = 0;

            if (string.IsNullOrWhiteSpace(this.ImageUrl) == false)
            {
                quality += 25;
            }

            if (this.ImageData?.Length > 0)
            {
                quality += 25;
            }

            if (string.IsNullOrWhiteSpace(this.Text) == false)
            {
                quality += 25;
            }

            if (string.IsNullOrWhiteSpace(this.HtmlText) == false)
            {
                quality += 25;
            }

            return quality;
        }
    }
}