using Facebook.Archive.Model.Page.Architecture;
using Facebook.Archive.Model.Page.Implementation.Base;

namespace Facebook.Archive.Model.Page.Implementation
{
    public class PagePostWithPhoto : GenericPagePost, IPagePhotoPost
    {
        public string ImageUrl { get; set; }

        public byte[] ImageData { get; set; }

        protected override int GetQualityInternal()
        {
            var quality = 0;

            if(string.IsNullOrWhiteSpace(this.ImageUrl) == false)
            {
                quality += 50;
            }

            if(this.ImageData?.Length > 0)
            {
                quality += 50;
            }

            return quality;
        }
    }
}
