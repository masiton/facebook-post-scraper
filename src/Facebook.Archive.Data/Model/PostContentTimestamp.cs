using Facebook.Archive.Data.Model.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Facebook.Archive.Data.Model
{
    [Table("PostContentTimestamps", Schema = "facebook")]
    public class PostContentTimestamp : ModelBase
    {
        [Required]
        public PostContent PostContent { get; set; }

        public DateTime TimestampUtc { get; set; }

        [Required]
        public string TimestampRaw { get; set; }
    }
}