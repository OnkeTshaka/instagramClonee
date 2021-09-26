using socials.Models;
using socials.Services;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace socials.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _ctx = new ApplicationDbContext();
        private readonly IPostService _postService;

        public HomeController(IPostService _postService)
        {
            this._postService = _postService;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var posts = await _postService.GetTimelinePosts(User.Identity.Name, _ctx);
            return View(posts);
        }
    }
}