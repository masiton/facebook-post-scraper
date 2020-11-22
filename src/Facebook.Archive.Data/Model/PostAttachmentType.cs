using Facebook.Archive.Data.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Facebook.Archive.Data.Model
{
    [Table("PostAttachments", Schema = "facebook")]
    public class PostAttachmentType : ModelBase
    {
        [Required]
        public string Name { get; set; }
    }
}