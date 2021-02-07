using Facebook.Archive.Data.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Facebook.Archive.Data.Model
{
    [Table("PostContentTexts", Schema = "facebook")]
    public class PostContentText : ModelBase
    {
        [Required]
        public PostContent PostContent { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public string Html { get; set; }
    }
}