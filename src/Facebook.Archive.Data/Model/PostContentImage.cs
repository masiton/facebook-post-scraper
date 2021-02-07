using Facebook.Archive.Data.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Facebook.Archive.Data.Model
{
    [Table("PostContentImages", Schema = "facebook")]
    public class PostContentImage : ModelBase
    {
        [Required]
        public PostContent PostContent { get; set; }

        [Required]
        public byte[] ImageData { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public string ImageUrlHtml { get; set; }
    }
}