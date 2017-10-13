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
                authors = x.Authors.Select(y => new
                {
                    name = y.Name
                })
            });
            return Json(jsonBook, JsonRequestBehavior.AllowGet);
        }

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
        public ActionResult Create()
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
                Authors = authors
                };
            var addBookToStore = new BooksInStore
            {
                Version = bookinfo.Version,
                ISBN = bookinfo.ISBN,
                Genre = bookinfo.Genre,
                SellingPrice = bookinfo.SellingPrice,
                RentingPrice = bookinfo.RentingPrice,
                AvailableTrade = bookinfo.AvailableTrade,
                Title = bookinfo.Title,
                Quantity = bookinfo.Quantity,
                Description = bookinfo.Description,
                PhotoUrl = bookinfo.PhotoUrl,
                Authors = bookinfo.Authors
            };
            var currentUserId = User.Identity.GetUserId();
            var up = db.UserProfiles.FirstOrDefault(x => x.UserId == currentUserId);
            if (bookinfo == null)
            {
                return Json(bookinfo);
            }
            else
            {
                up.BooksInStore.Add(addBookToStore);
                bookinfo.BookSellerId = up.UserId;
                db.Books.Add(bookinfo);
                db.SaveChanges();

                return Json(bookinfo);
            }

           
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

            var bookRent = new BookRental
            {
                BookId = book.BookId,
                Title = book.Title,
                Description = book.Description,
                Version = book.Version,
                ISBN = book.ISBN,
                Genre = book.Genre,
                RentingPrice = book.RentingPrice
            };
            
            var currentUserId = User.Identity.GetUserId();
            var up = db.UserProfiles.FirstOrDefault(x => x.UserId == currentUserId);
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


        //// GET: Books/Edit/5
        //[System.Web.Http.HttpDelete]
        //[System.Web.Http.Route("Books/SellBook/{id}")]
        //public JsonResult SellBook(int? id)
        //{
        //    if (id == null)
        //    {
        //        return Json("Fail No Id provided");
        //    }
        //    Book book = db.Books.Find(id);
        //    if (book.Quantity > 1)
        //    {
        //        book.Quantity -= 1;
        //    }
        //    else
        //    {
        //        db.Books.Remove(book);
        //    }
        //    var currentUserId = User.Identity.GetUserId();
        //    var up = db.UserProfiles.FirstOrDefault(x => x.UserId == book.BookSellerId);
        //    if (book == null)
        //    {
        //        return Json("Fail no Book");
        //    }
        //    else
        //    {
        //        book.Quantity = 1;
        //        up.BooksSold.Add(book);
        //        db.SaveChanges();
        //        return Json("success");
        //    }

        //}

        // POST: Books/Delete/5
        [System.Web.Http.HttpDelete]
        [System.Web.Http.Route("Books/deleteBook/{id}")]
        public HttpStatusCode DeleteBook(int id)
        {
            Book book = db.Books.Find(id);
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
