using socials.Models;
using System.Data.Entity;
using System.Threading.Tasks;

namespace socials.Repository
{
    public class UserRepository : IUserRepository
    {
        public async Task<ApplicationUser> GetByUsername(string userName, ApplicationDbContext _ctx)
        {
            return await _ctx.Users.FirstOrDefaultAsync(u => u.UserName == userName);
        }

        public async Task<ApplicationUser> GetByUsernameOrId(string userName, ApplicationDbContext _ctx)
        {
            return await _ctx.Users.FirstOrDefaultAsync(u => u.UserName == userName || u.Id == userName);
        }
    }

    public interface IUserRepository
    {
        Task<ApplicationUser> GetByUsername(string userName, ApplicationDbContext _ctx);
        Task<ApplicationUser> GetByUsernameOrId(string userName, ApplicationDbContext _ctx);
    }
}
