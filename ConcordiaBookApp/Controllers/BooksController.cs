using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ConcordiaBookApp.Models;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using static ConcordiaBookApp.Controllers.UserController;

namespace ConcordiaBookApp.Controllers
{
    public class BooksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

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
                quantity = x.Quantity,
                AvailableTrade = x.AvailableTrade,
                rating = x.sellerRating,
                authors = x.Authors.Select(y => new
                {
                    name = y.Name
                })
            });
            return Json(jsonBook, JsonRequestBehavior.AllowGet);
        }
        [System.Web.Mvc.Authorize]
        public ActionResult Index()
        {
            return View();
        }

        // GET: Books/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: Books/Create
        [System.Web.Http.Route("Books/create")]
        public ActionResult Create()
        {
            return View();
        }

        // GET: Books/Create
        [System.Web.Http.Route("Books/add")]
        public ActionResult Add()
        {
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [System.Web.Http.HttpPost]
        [System.Web.Http.Authorize]
        [System.Web.Http.Route("Books/postBook")]
        public JsonResult PostBook([FromBody]PostBook book)
        {

                var authors = new List<Author>();
                var author = db.Authors.FirstOrDefault(a => a.Name == book.AuthorName) ?? new Author { Name = book.AuthorName };
                authors.Add(author);

            var bookinfo = new Book
            {
                Version = book.Version,
                ISBN = book.ISBN,
                Genre = book.Genre,
                SellingPrice = book.SellingPrice,
                RentingPrice = book.RentingPrice,
                AvailableTrade = book.AvailableTrade,
                Title = book.Title,
                Quantity = book.Quantity,
                Description = book.Description,
                PhotoUrl = book.PhotoUrl,
                Authors = authors,
                };

            var currentUserId = User.Identity.GetUserId();
            var up = db.UserProfiles.FirstOrDefault(x => x.UserId == currentUserId);
            var addBookToStore = new BooksInStore
            {
                Book = bookinfo,
                user = up
            };
            var bookstore = new List<BooksInStore>();
            bookstore.Add(addBookToStore);
            if (bookinfo == null)
            {
                return Json(bookinfo);
            }
            else
            { 
                bookinfo.BookStore = bookstore;
                db.Books.Add(bookinfo);
                db.SaveChanges();

                return Json(bookinfo);
            }

           
        }

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [System.Web.Http.HttpPost]
        [System.Web.Http.Authorize]
        [System.Web.Http.Route("Books/rateuser")]
        public JsonResult RateUser([FromBody]PostRating newRating)
        {
            var test = db.BooksInStore.FirstOrDefault(y => y.Book.BookId == newRating.BookId);
            var bookOwner = db.UserProfiles.Find(test.user.UserId);
            var ratingList = new List<UserRatings>();
            var rating = new UserRatings();
            rating.Rating = newRating.Rating;
            ratingList.Add(rating);
            bookOwner.MyRating = ratingList;
            var currentBook = db.Books.FirstOrDefault(x => x.BookId == newRating.BookId);
            currentBook.sellerRating = newRating.Rating;
            db.SaveChanges();

            return Json(HttpStatusCode.OK);
            


        }


        // GET: Books/Edit/5
        [System.Web.Http.HttpDelete]
        [System.Web.Http.Route("Books/RentBook/{id}")]
        public string RentBook(int? id)
        {
            if (id == null)
            {
               return "Fail No Id provided";
            }
            Book book = db.Books.Find(id);
            book.Quantity -= 1;

          
            
            var currentUserId = User.Identity.GetUserId();
            var up = db.UserProfiles.FirstOrDefault(x => x.UserId == currentUserId);

            var bookRent = new BookRental
            {
                owner = up,
                RentedBook = book
            };

            if (book == null)
            {
                return "Fail no Book";
            }
            else
            {
                up.BookRentals.Add(bookRent);
                
                db.SaveChanges();
                return "success";    
            }
            
        }

        // GET: Books/Edit/5
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("Books/SendMessage/{id}")]
        public string SendMessage(int id, [FromBody]MessageDto messages)
        {
            try
            {
                var currentUserId = User.Identity.GetUserId();
                var up = db.UserProfiles.FirstOrDefault(x => x.UserId == currentUserId);
                var test = db.BooksInStore.FirstOrDefault(y => y.Book.BookId == id);
                var bookOwner = db.UserProfiles.Find(test.user.UserId);

                var messageThreadList = new List<MessageThread>();
                var messageThread = new MessageThread();
                messageThread.Title = messages.Title;
                messageThread.MessageBody = messages.MessageBody;
                messageThread.SenderId = up.UserId;
                messageThreadList.Add(messageThread);

                var message = new Messages();                
                message.FromId = up.UserId;
                message.MessagesInThread = messageThreadList;
                message.User = bookOwner;
                bookOwner.Messages.Add(message);

                db.SaveChanges();
            } 
            catch(Exception err)
            {
                return err.Message;
            }
            return "success";

        }


        // POST: Books/Delete/5
        [System.Web.Http.HttpDelete]
        [System.Web.Http.Route("Books/deleteBook/{id}")]
        public HttpStatusCode DeleteBook(int id)
        {
            Book book = db.Books.Find(id);
            BooksInStore books = db.BooksInStore.FirstOrDefault(x => x.Book.BookId == id);
            db.BooksInStore.Remove(books);
            db.Books.Remove(book);
            db.SaveChanges();
            return HttpStatusCode.OK;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
