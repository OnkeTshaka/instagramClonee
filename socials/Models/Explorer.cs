using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace socials.Models
{
    [Table("Explore")]
    public class Explorer
    {

        [Key, ScaffoldColumn(false), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public virtual ApplicationUser User { get; set; }

        public DateTime Timestamp { get; set; }

        public string Image { get; set; }

        public byte[] video { get; set; }

        public string Description { get; set; }

        public virtual List<Comment> Comments { get; set; }

        public virtual List<Like> Likes { get; set; }
    }
}