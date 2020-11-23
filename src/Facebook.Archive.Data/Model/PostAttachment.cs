using Facebook.Archive.Data.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Facebook.Archive.Data.Model
{
    [Table("PostAttachments", Schema = "facebook")]
    public class PostAttachment : ModelBase
    {
        [Required]
        public PostAttachmentType Type { get; set; }

        [Required]
        public byte[] Data { get; set; }

        public string Description { get; set; }

        public string Url { get; set; }
    }
}