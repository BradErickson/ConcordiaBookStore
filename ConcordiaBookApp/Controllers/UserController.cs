using ConcordiaBookApp.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ConcordiaBookApp.Controllers
{


    public class UserController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ActionResult index()
        {
            return View();
        }

        public ActionResult UserProfile()
        {
            return View();
        }

        // GET: Books
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("Books/getBook")]
        public JsonResult GetBooks()
        {
            var result = db.Books.ToList();
            var jsonBook = result.Select(x => new
            {
                id = x.BookId,
                title = x.Title,
                isbn = x.ISBN,
                genre = x.Genre,
                rentingPrice = x.RentingPrice,
                sellingPrice = x.SellingPrice,
                photoUrl = x.PhotoUrl,
                authors = x.Authors.Select(y => new
                {
                    name = y.Name
                })
            });
            return Json(jsonBook, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        [System.Web.Http.Route("User/GetCurrentUser")]
        public JsonResult GetCurrentUser() 
        {
            var currentUserId = User.Identity.GetUserId();
            var result = db.UserProfiles.FirstOrDefault(x => x.UserId == currentUserId);
            var currentUserProfile = new UserProfile
            {
                FirstName = result.FirstName,
                LastName = result.LastName,
                Phone = result.Phone,
                Address = result.Address,
                City = result.City,
                State = result.State,
                ZipCode = result.ZipCode

            };
            return Json(currentUserProfile, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        [System.Web.Http.Route("User/RegisterNewUser")]
        public async Task<JsonResult> RegisterNewUser([System.Web.Http.FromBody]CreateUserViewModel model)
        {

            var profile = new UserProfile
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Phone = model.Phone,
                Address = model.Address,
                City = model.City,
                State = model.State,
                ZipCode = model.ZipCode
            };
            var user = new ApplicationUser { UserName = model.Email, Email = model.Email, UserProfile = profile };
            var result = await UserManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                return Json(profile);
            }

            return Json(profile);
        }
    }
}