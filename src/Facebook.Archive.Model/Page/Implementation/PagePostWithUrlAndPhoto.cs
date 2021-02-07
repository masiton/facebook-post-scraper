using Facebook.Archive.Model.Page.Architecture;
using Facebook.Archive.Model.Page.Implementation.Base;

namespace Facebook.Archive.Model.Page.Implementation
{
    public class PagePostWithUrlAndPhoto : GenericPagePost, IPageUrlPost, IPagePhotoPost
    {
        public string LinkUrl { get; set; }

        public string LinkText { get; set; }

        public string LinkUrlRaw { get; set; }

        public string ImageUrl { get; set; }

        public byte[] ImageData { get; set; }

        protected override int GetQualityInternal()
        {
            int quality = 0;

            if(string.IsNullOrWhiteSpace(this.LinkUrl) == false)
            {
                quality += 25;
            }

            if(string.IsNullOrWhiteSpace(this.LinkText) == false)
            {
                quality += 25;
            }

            if (string.IsNullOrWhiteSpace(this.ImageUrl) == false)
            {
                quality += 25;
            }

            if (this.ImageData?.Length > 0)
            {
                quality += 25;
            }

            return quality;
        }
    }
}