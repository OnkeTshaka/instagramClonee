using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace socials.Models
{
    [Table("Likes")]
    public class Like
    {
        [Key]
        public int Id { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual Post Post { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
