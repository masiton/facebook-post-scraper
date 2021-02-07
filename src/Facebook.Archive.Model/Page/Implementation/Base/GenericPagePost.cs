using Facebook.Archive.Model.Page.Architecture.Base;
using System;

namespace Facebook.Archive.Model.Page.Implementation.Base
{
    public abstract class GenericPagePost : IPagePost
    {
        public string Parser { get; set; }

        public int ParserVersion { get; set; }

        public string Url { get; set; }

        public string Html { get; set; }

        public DateTime? Timestamp { get; set; }

        public string TimestampRaw { get; set; }

        public int GetQuality()
        {
            var quality = 0;

            if (string.IsNullOrWhiteSpace(this.Url) == false)
            {
                quality += 33;
            }

            if(string.IsNullOrWhiteSpace(this.Html) == false)
            {
                quality += 33;
            }

            if(Timestamp.HasValue == true)
            {
                quality += 33;
            }

            quality += this.GetQualityInternal();

            return Convert.ToInt32((double)quality / 2);
        }

        protected abstract int GetQualityInternal();
    }
}