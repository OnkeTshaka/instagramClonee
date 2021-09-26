using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace socials.Models
{
    [Table("Comments")]
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(150, ErrorMessage="Your comment must be less than 150 characters")]
        public string Message { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual Post Post { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
