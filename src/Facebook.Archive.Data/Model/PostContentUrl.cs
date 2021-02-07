using Facebook.Archive.Data.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Facebook.Archive.Data.Model
{
    [Table("PostContentUrls", Schema = "facebook")]
    public class PostContentUrl : ModelBase
    {
        [Required]
        public PostContent PostContent { get; set; }

        [Required]
        public string Url { get; set; }

        public string Text { get; set; }
    }
}