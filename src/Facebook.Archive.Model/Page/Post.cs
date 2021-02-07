using Facebook.Archive.Model.Base;
using Facebook.Archive.Model.Page.Parts;
using System;
using System.Collections.Generic;

namespace Facebook.Archive.Model.Page
{
    public class Post : FacebookElement
    {
        public string Url { get; set; }

        public string Html { get; set; }

        public DateTime? Timestamp { get; set; }

        public string TimestampRaw { get; set; }

        public List<PostImage> Images { get; set; }

        public List<PostLink> Links { get; set; }

        public List<PostText> Texts { get; set; }

        public string SharedPostUrl { get; set; }

        public Post()
        {
            this.Images = new List<PostImage>();
            this.Links = new List<PostLink>();
            this.Texts = new List<PostText>();
        }
    }
}