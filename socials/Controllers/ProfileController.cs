using socials.Models;
using socials.Services;
using socials.ViewModels;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace socials.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly ApplicationDbContext _ctx = new ApplicationDbContext();
        private readonly IUserService _userService;
        private readonly IFollowService _followService;

        public ProfileController(IUserService _userService,
                                IFollowService _followService)
        {
            this._userService = _userService;
            this._followService = _followService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> Details(string id)
        {
            var userId = id ?? string.Empty;

            // Don't try load a profile called /profile/
            if (id.ToLower() == "profile" && Request.IsAuthenticated && User.Identity.Name != "profile")
                userId = string.Empty;

            // No user ID provided, show user's profile if currently logged in
            if (string.IsNullOrEmpty(userId) && Request.IsAuthenticated)
            {
                var loggedInUser = await _userService.GetByUsername(User.Identity.Name, _ctx);
                userId = loggedInUser.Id;
            }

            // Find profile details
            var user = await _userService.GetByUsernameOrId(userId, _ctx);
            if (user == null) return Redirect("~/");

            var model = new ProfileViewModel
            {
                User = user,
                PostCount = user.Posts != null && user.Posts.Any() ? user.Posts.Count : 0,
                FollowerCount = await _followService.FollowerCount(user.UserName, _ctx),
                FollowingCount = await _followService.FollowingCount(user.UserName, _ctx),
                Following = false,
                OwnProfile = false
            };

            if (Request.IsAuthenticated)
            {
                model.Following = await _followService.IsFollowing(User.Identity.Name, user.UserName, _ctx);
                model.OwnProfile = user.UserName == User.Identity.Name;
            }

            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> Following(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var following = await _followService.GetFollowing(id, _ctx);
            ViewBag.Message = String.Format("Following", id);

            return PartialView("_UserList", following);
        }

        [HttpGet]
        public async Task<ActionResult> Followers(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var followers = await _followService.GetFollowers(id, _ctx);
            ViewBag.Message = String.Format("Followers", id);

            return PartialView("_UserList", followers);
        }

        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            ApplicationUser member = _ctx.Users.Find(id);
            id = User.Identity.Name;
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // POST: Members/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserName,Email,RealName,Bio,Photo")] ApplicationUser member, HttpPostedFileBase ProfilePictureFile)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser memberInDB = _ctx.Users.Single(c => c.Id == member.Id);
                if (ProfilePictureFile != null)
                {
                    string uploadDirectory = Server.MapPath(String.Format("~/Images/Profiles/{0}/", User.Identity.Name));
                    if (!Directory.Exists(uploadDirectory))
                        Directory.CreateDirectory(uploadDirectory);
                    var path = Path.Combine(uploadDirectory, ProfilePictureFile.FileName);
                    ProfilePictureFile.SaveAs(path);
                    member.Photo = (ProfilePictureFile.FileName);
                    memberInDB.Photo = member.Photo;
                }


                memberInDB.Email = member.Email;
                memberInDB.UserName = member.UserName;
                memberInDB.RealName = member.RealName;
                memberInDB.Bio = member.Bio;
                _ctx.SaveChanges();
                return RedirectToAction("Details", "Profile", new { id = User.Identity.Name });
            }
            return View(member);
        }

        // GET: Members/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser member = _ctx.Users.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }
    }
}