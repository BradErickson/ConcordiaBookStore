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

namespace ConcordiaBookApp.Controllers
{
    public class MessagesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Create(int? ID)
        {
            return View();
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("Messages/ReplyMessage/{id}")]
        public string ReplyToMessage(int id, [FromBody]MessageDto message)
        {
            try
            {
                var currentUserId = User.Identity.GetUserId();
                var getMessageThread = db.Messages.Find(id);

                var newMessage = new MessageThread();
                newMessage.Title = message.Title;
                newMessage.MessageBody = message.MessageBody;
                newMessage.SenderId = currentUserId;

                getMessageThread.MessagesInThread.Add(newMessage);
                db.SaveChanges();
            }
            catch (Exception err)
            {
                return err.Message;
            }
            return "success";

        }
        // POST: Messages/Delete/5
        [System.Web.Http.HttpPost, System.Web.Http.ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Messages messages = db.Messages.Find(id);
            db.Messages.Remove(messages);
            db.SaveChanges();
            return RedirectToAction("Index");
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
