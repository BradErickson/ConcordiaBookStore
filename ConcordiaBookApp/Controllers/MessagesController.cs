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
        public ActionResult MyMessages()
        {
            return View();
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("Messages/GetMyMessages")]
        public JsonResult GetMyMessages()
        {
            try
            {
                var currentUserId = User.Identity.GetUserId();
                var message = db.Messages.Where(x => x.User.UserId == currentUserId).ToList();
                var messageReturnList = new List<GetMessageDTO>();
                foreach(var m in message)
                {
                    var messageThread = db.MessagesInThread.FirstOrDefault(y => y.MessageId == m.MessageID);
                    var fromUser = db.UserProfiles.FirstOrDefault(z => z.UserId == m.FromId);
                    var messages = new GetMessageDTO();
                    messages.MessageThreadID = m.MessageID;
                    messages.FromName = fromUser.FirstName + " " + fromUser.LastName;
                    messages.SubjectLine = messageThread.Title;
                    messages.MessageBody = messageThread.MessageBody;
                    messageReturnList.Add(messages);
                }
                
               
                 return Json(messageReturnList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception err)
            {
                return Json(err.Message);
            }

        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("Messages/ReplyToMessage/{id}")]
        public string ReplyToMessage(int id, [FromBody]MessageDto message)
        {
            try
            {
                var currentUserId = User.Identity.GetUserId();
                var getMessageThread = db.MessagesInThread.Where(x => x.MessageId == id).ToList();

                var newMessage = new MessageThread();
                newMessage.Title = message.Title;
                newMessage.MessageBody = message.MessageBody;
                newMessage.SenderId = currentUserId;

                getMessageThread.Add(newMessage);
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
