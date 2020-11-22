using Facebook.Archive.Data.Model.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Facebook.Archive.Data.Model
{
    [Table("Updates", Schema = "facebook")]
    public class Update : ModelBase
    {
        [Required]
        public DateTime StartUtc { get; set; }

        public DateTime? EndUtc { get; set; }

        public bool? IsSuccessful { get; set; }
    }
}