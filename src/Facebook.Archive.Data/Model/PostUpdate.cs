using Facebook.Archive.Data.Model.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Facebook.Archive.Data.Model
{
    [Table("PostUpdates", Schema = "facebook")]
    public class PostUpdate : ModelBase
    {
        [Required]
        public Update Update { get; set; }

        [Required]
        public Post Post { get; set; }

        [Required]
        public DateTime Timestamp { get; set; }
    }
}