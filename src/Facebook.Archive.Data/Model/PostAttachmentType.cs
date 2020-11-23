using Facebook.Archive.Data.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Facebook.Archive.Data.Model
{
    [Table("PostAttachmentTypes", Schema = "facebook")]
    public class PostAttachmentType : ModelBase
    {
        public const string TYPE_IMAGE = "IMAGE";

        [Required]
        public string Name { get; set; }
    }
}