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
                var getMessageThreads = db.Messages.Where(x => x.User.UserId == currentUserId || x.FromId == currentUserId).ToList();
                
                var messageDTO = new List<GetMessageDTO>();
                foreach (var i in getMessageThreads)
                {
                    var getMessagesInThread = db.MessagesInThread.Where(x => x.MessageId == i.MessageID).ToList();
                    var mess = new GetMessageDTO();
                    mess.MessageThreadID = i.MessageID;
                    mess.Message = new List<MessageThread>();
                    foreach(var j in getMessagesInThread)
                    {
                        var newMessage = new MessageThread();
                        
                        var user = db.UserProfiles.FirstOrDefault(z => z.UserId == j.SenderId);
                        newMessage.MessageBody = j.MessageBody;
                        newMessage.Title = j.Title;
                        newMessage.SenderId = j.SenderId;
                        newMessage.SenderName = user.FirstName + " " + user.LastName;
                        mess.Message.Add(newMessage);
                    }
                    messageDTO.Add(mess);
                }
               
                 return Json(messageDTO, JsonRequestBehavior.AllowGet);
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
                var getMessageThread = db.MessagesInThread.FirstOrDefault(x => x.MessageId == id);

                var newMessage = new MessageThread();
                newMessage.Title = message.Title;
                newMessage.MessageBody = message.MessageBody;
                newMessage.SenderId = currentUserId;
                newMessage.MessageId = getMessageThread.MessageId;
                var messageT = db.Messages.FirstOrDefault(x => x.MessageID == getMessageThread.MessageId);
                messageT.MessagesInThread.Add(newMessage);
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
