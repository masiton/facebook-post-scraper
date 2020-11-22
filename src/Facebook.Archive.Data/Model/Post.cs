using Facebook.Archive.Data.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Facebook.Archive.Data.Model
{
    [Table("Posts", Schema = "facebook")]
    public class Post : ModelBase
    {
        [Required]
        public string Url { get; set; }
    }
}