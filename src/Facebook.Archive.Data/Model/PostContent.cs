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

        public string Text { get; set; }

        [Required]
        public string RawHtml { get; set; }

        public List<PostAttachment> Attachments { get; set; }
    }
}