using Facebook.Archive.Data.Model.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Facebook.Archive.Data.Model
{
    [Table("PostContents", Schema = "facebook")]
    public class PostContent : ModelBase
    {
        [Required]
        public PostUpdate PostUpdate { get; set; }

        [Required]
        public string ParserName { get; set; }

        [Required]
        public int ParserVersion { get; set; }

        [Required]
        public string Html { get; set; }

        public List<PostContentText> Texts { get; set; }

        public List<PostContentPhoto> Images { get; set; }

        public List<PostContentUrl> Links { get; set; }

        public List<PostContentTimestamp> Timestamps { get; set; }
    }
}