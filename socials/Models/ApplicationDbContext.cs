using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace socials.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        //Models
        public DbSet<Post> Posts { get; set; }
        public DbSet<Explorer> Explore { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Following> Following { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        public virtual DbSet<Direct_Message> Direct_Messages { get; set; }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}