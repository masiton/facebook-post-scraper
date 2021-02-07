using Facebook.Archive.Model.Page.Architecture;
using Facebook.Archive.Model.Page.Implementation.Base;

namespace Facebook.Archive.Model.Page.Implementation
{
    public class PagePostWithUrl : GenericPagePost, IPageUrlPost
    {
        public string LinkUrl { get; set; }

        public string LinkUrlRaw { get; set; }

        public string LinkText { get; set; }

        protected override int GetQualityInternal()
        {
            int quality = 0;

            if(string.IsNullOrWhiteSpace(this.LinkUrl) == false)
            {
                quality += 50;
            }

            if(string.IsNullOrWhiteSpace(this.LinkText) == false)
            {
                quality += 50;
            }

            return quality;
        }
    }
}