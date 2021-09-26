using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace socials.Models
{
    public class Direct_Message
    {

        [Key, ScaffoldColumn(false), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(150, ErrorMessage = "Your comment must be less than 150 characters")]
        public string Message { get; set; }

        public virtual ApplicationUser User { get; set; }

        public DateTime Timestamp { get; set; }
    }
}