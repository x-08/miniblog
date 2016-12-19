using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using miniBlog.Models;

namespace miniBlog.Controllers
{
    public class MessageController :  BaseController
    {
        private MessageContext db = new MessageContext();

        //
        // GET: /Message/

        public ActionResult Index()
        {
            return View(db.MessageModels.ToList());
        }

        //
        // GET: /Message/Details/5

        public ActionResult Details(int id = 0)
        {
            MessageModel messagemodel = db.MessageModels.Find(id);
            if (messagemodel == null)
            {
                return HttpNotFound();
            }
            return View(messagemodel);
        }

        //
        // GET: /Message/Create

        public ActionResult Create()
        {
            ViewBag.name = "message";
            return View();
        }

        //
        // POST: /Message/Create

        [HttpPost]
 
        public ActionResult Create(MessageModel messagemodel)
        {
            messagemodel.Time = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
            if (ModelState.IsValid)
            {
                db.MessageModels.Add(messagemodel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(messagemodel);
        }

        //
        // GET: /Message/Edit/5

        public ActionResult Edit(int id = 0)
        {
            MessageModel messagemodel = db.MessageModels.Find(id);
            if (messagemodel == null)
            {
                return HttpNotFound();
            }
            return View(messagemodel);
        }

        //
        // POST: /Message/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MessageModel messagemodel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(messagemodel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(messagemodel);
        }

        //
        // GET: /Message/Delete/5

        public ActionResult Delete(int id = 0)
        {
            MessageModel messagemodel = db.MessageModels.Find(id);
            if (messagemodel == null)
            {
                return HttpNotFound();
            }
            return View(messagemodel);
        }

        //
        // POST: /Message/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MessageModel messagemodel = db.MessageModels.Find(id);
            db.MessageModels.Remove(messagemodel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}