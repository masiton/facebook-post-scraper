using Facebook.Archive.Data.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Facebook.Archive.Data.Model
{
    [Table("Pages", Schema = "facebook")]
    public class Page : ModelBase
    {
        [Required]
        public string Url { get; set; }
    }
}