namespace Facebook.Archive.Model.Page.Parts
{
    public class PostImage
    {
        public string ImageUrlHtml { get; set; }

        public string ImageUrl { get; set; }

        public byte[] ImageData { get; set; }

        /// <summary>
        /// If we recognize and store this flag we can use 3rd party tools to download the videos.
        /// </summary>
        public bool IsVideo { get; set; }
    }
}