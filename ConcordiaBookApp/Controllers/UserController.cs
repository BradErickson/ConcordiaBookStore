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

        [HttpGet]
        [System.Web.Http.Route("User/GetCurrentUser")]
        public JsonResult GetCurrentUser() 
        {
            try
            {
                var currentUserId = User.Identity.GetUserId();
                var result = db.UserProfiles.FirstOrDefault(x => x.UserId == currentUserId);
                var currentUserProfile = new GetUser
                {
                    FirstName = result.FirstName,
                    LastName = result.LastName,
                    Phone = result.Phone,
                    Address = result.Address,
                    City = result.City,
                    State = result.State,
                    ZipCode = result.ZipCode,
                    BookRentals = new List<BookRent>(),
                    BooksForSale = new List<BookStore>()
                };
                foreach (var r in result.BookRentals)
                {
                    var b = new BookRent();
                    b.Title = r.RentedBook.Title;
                    b.Description = r.RentedBook.Description;
                    b.Version = r.RentedBook.Version;
                    b.ISBN = r.RentedBook.ISBN;
                    b.Genre = r.RentedBook.Genre;
                    b.SellingPrice = r.RentedBook.SellingPrice;
                    b.RentingPrice = r.RentedBook.RentingPrice;
                    b.AvailableTrade = r.RentedBook.AvailableTrade;

                    currentUserProfile.BookRentals.Add(b);
                }
                foreach (var a in result.BooksInStore)
                {
                    var c = new BookStore();
                    c.Title = a.Book.Title;
                    c.Description = a.Book.Description;
                    c.Version = a.Book.Version;
                    c.ISBN = a.Book.ISBN;
                    c.Genre = a.Book.Genre;
                    c.SellingPrice = a.Book.SellingPrice;
                    c.RentingPrice = a.Book.RentingPrice;
                    c.AvailableTrade = a.Book.AvailableTrade;
                    currentUserProfile.BooksForSale.Add(c);
                }

                return Json(currentUserProfile, JsonRequestBehavior.AllowGet);

            } catch(Exception ex)
            {
                return Json(ex);
            }

        }
        public class BookRent {
            public string Title { get; set; }
            public string Description { get; set; }
            public double Version { get; set; }
            public int ISBN { get; set; }
            public string Genre { get; set; }
            public double SellingPrice { get; set; }
            public double RentingPrice { get; set; }
            public bool AvailableTrade { get; set; }
        }
        
        public class BookStore
        {
            public string Title { get; set; }
            public string Description { get; set; }
            public double Version { get; set; }
            public int ISBN { get; set; }
            public string Genre { get; set; }
            public double SellingPrice { get; set; }
            public double RentingPrice { get; set; }
            public bool AvailableTrade { get; set; }
        }
        public class GetUser
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Phone { get; set; }
            public string Address { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public int ZipCode { get; set; }
            public List<BookRent> BookRentals { get; set; }
            public List<BookStore> BooksForSale { get; set; }
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
                ZipCode = model.ZipCode,
                
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