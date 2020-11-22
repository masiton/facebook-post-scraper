using Facebook.Archive.Data.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Facebook.Archive.Data.Model
{
    [Table("PostAttachmentTypes", Schema = "facebook")]
    public class PostAttachment : ModelBase
    {
        [Required]
        public PostAttachmentType Type { get; set; }
    }
}