using Facebook.Archive.Data.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Facebook.Archive.Data.Model
{
    [Table("PostContentLinks", Schema = "facebook")]
    public class PostContentLink : ModelBase
    {
        [Required]
        public PostContent PostContent { get; set; }

        [Required]
        public string Url { get; set; }

        [Required]
        public string UrlHtml { get; set; }

        public string Text { get; set; }
    }
}