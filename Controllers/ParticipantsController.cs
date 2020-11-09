using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CaringSquareApp.Models;

namespace CaringSquareApp.Controllers
{
    /*
         * Controller not Part of Application
         * Can be used for Future Scope
         * To Maintain Participant List for a Created Event
         */
    [Authorize(Roles = "Administrator")]
    public class ParticipantsController : Controller
    {
        private CaringSquareEntities db = new CaringSquareEntities();

        // GET: Participants
        public ActionResult Index()
        {
            var participants = db.Participants.Include(p => p.AspNetUser).Include(p => p.SocialEvent);
            return View(participants.ToList());
        }

        // GET: Participants/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Participant participant = db.Participants.Find(id);
            if (participant == null)
            {
                return HttpNotFound();
            }
            return View(participant);
        }

        // GET: Participants/Create
        public ActionResult Create()
        {
            ViewBag.UserUserId = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.SocialEventEventId = new SelectList(db.SocialEvents, "EventId", "EventName");
            return View();
        }

        // POST: Participants/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ParticipantId,UserUserId,SocialEventEventId")] Participant participant)
        {
            if (ModelState.IsValid)
            {
                db.Participants.Add(participant);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserUserId = new SelectList(db.AspNetUsers, "Id", "Email", participant.UserUserId);
            ViewBag.SocialEventEventId = new SelectList(db.SocialEvents, "EventId", "EventName", participant.SocialEventEventId);
            return View(participant);
        }

        // GET: Participants/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Participant participant = db.Participants.Find(id);
            if (participant == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserUserId = new SelectList(db.AspNetUsers, "Id", "Email", participant.UserUserId);
            ViewBag.SocialEventEventId = new SelectList(db.SocialEvents, "EventId", "EventName", participant.SocialEventEventId);
            return View(participant);
        }

        // POST: Participants/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ParticipantId,UserUserId,SocialEventEventId")] Participant participant)
        {
            if (ModelState.IsValid)
            {
                db.Entry(participant).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserUserId = new SelectList(db.AspNetUsers, "Id", "Email", participant.UserUserId);
            ViewBag.SocialEventEventId = new SelectList(db.SocialEvents, "EventId", "EventName", participant.SocialEventEventId);
            return View(participant);
        }

        // GET: Participants/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Participant participant = db.Participants.Find(id);
            if (participant == null)
            {
                return HttpNotFound();
            }
            return View(participant);
        }

        // POST: Participants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Participant participant = db.Participants.Find(id);
            db.Participants.Remove(participant);
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
