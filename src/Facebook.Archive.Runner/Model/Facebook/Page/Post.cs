using System;

namespace Facebook.Archive.Runner.Model.Facebook.Page
{
    public class Post
    {
        public DateTime TimestampUtc { get; set; }

        public string Url { get; set; }

        public string Text { get; set; }

        public string Html { get; set; }

        public int Likes { get; set; }
    }
}
