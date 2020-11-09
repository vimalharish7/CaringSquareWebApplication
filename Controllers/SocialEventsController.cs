using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CaringSquareApp.Models;
using CaringSquareApp.Utils;
using Microsoft.AspNet.Identity;

namespace CaringSquareApp.Controllers
{
    public class SocialEventsController : Controller
    {
        private CaringSquareEntities db = new CaringSquareEntities();

        /*
         * Name: Social Events Page - Index()
         * First Function implemented when Social Events is Clicked 
         * No Arguments
         * Returns all Social Events of Sepcific User 
         */
        // GET: SocialEvents
        [Authorize]
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var eventLists = db.SocialEvents.Where(s => s.UserUserId == userId).ToList();
            //var socialEvents = db.SocialEvents.Include(s => s.AspNetUser).Include(s => s.POIs);
            return View(eventLists);
        }

        /*
         * Name: Social Events Page - Details()
         * Function implemented when Details of a Social Event is Clicked 
         * Arguments - Event ID
         * Returns Details of Social Event 
         */
        // GET: SocialEvents/Details/5
        public ActionResult Details(int? id)
        {
            var userId = User.Identity.GetUserId();
            var eventLists = db.SocialEvents.Where(s => s.UserUserId == userId).ToList();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SocialEvent socialEvent = db.SocialEvents.Find(id);
            if (socialEvent == null)
            {
                return HttpNotFound();
            }
            bool alreadyExist = eventLists.Contains(socialEvent);
            if(alreadyExist == true)
            {
                return View(socialEvent);
            }
            else
            {
                return RedirectToAction("AccessDenied");
            }

        }

        /*
         * Name: Social Events Page - Details()
         * Function implemented to show warning message to restrict Users from accessing other User's Events 
         * Returns Access Denied page - 401 Error 
         */
        public ActionResult AccessDenied()
        {
            return View();
        }

        /*
         * Name: Social Events Page - Create()
         * Function implemented to allow User to Create New Social Event 
         * Arguments - No
         * Returns Create Event Page 
         */
        [Authorize]
        // GET: SocialEvents/Create
        public ActionResult Create()
        {
            
            ViewBag.UserUserId = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.POIPlaceId = new SelectList(db.POIs, "PlaceId", "Name");

            return View();
        }

        // POST: SocialEvents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        /*
         * Name: Social Events Page - Create()
         * Function implemented to save values to DB 
         * Arguments - Values Entered in Front End
         * Save values to DB 
         */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EventId,EventName,EventDate,EventTime,UserUserId,POIPlaceId")] SocialEvent socialEvent)
        {
            if (ModelState.IsValid)
            {
                db.SocialEvents.Add(socialEvent);
                db.SaveChanges();
                return Redirect("/SocialEvents?Success=Create");
            }

            ViewBag.UserUserId = new SelectList(db.AspNetUsers, "Id", "Email", socialEvent.UserUserId);
            ViewBag.POIPlaceId = new SelectList(db.POIs, "PlaceId", "Name", socialEvent.POIPlaceId);
            
            return View(socialEvent);
        }

        /*
         * Name: Social Events Page - Edit()
         * Function implemented to allow User to Edit Social Event 
         * Arguments - Event Id
         * Returns Index Page after Save  
         */
        [Authorize]
        // GET: SocialEvents/Edit/5
        public ActionResult Edit(int? id)
        {
            var userId = User.Identity.GetUserId();
            var eventLists = db.SocialEvents.Where(s => s.UserUserId == userId).ToList();
           
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SocialEvent socialEvent = db.SocialEvents.Find(id);
            if (socialEvent == null)
            {

                return HttpNotFound();
            }
            bool alreadyExist = eventLists.Contains(socialEvent);
            if (alreadyExist == true)
            {
                ViewBag.UserUserId = new SelectList(db.AspNetUsers, "Id", "Email", socialEvent.UserUserId);
                ViewBag.POIPlaceId = new SelectList(db.POIs, "PlaceId", "Name", socialEvent.POIPlaceId);
                return View(socialEvent);
            }
            else
            {
                return RedirectToAction("AccessDenied");
            }
            
        }

        // POST: SocialEvents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        /*
         * Name: Social Events Page - Edit()
         * Function implemented to save values to DB 
         * Arguments - Values Entered in Front End
         * Save values to DB 
         */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EventId,EventName,EventDate,EventTime,UserUserId,POIPlaceId")] SocialEvent socialEvent)
        {
            
            if (ModelState.IsValid)
            {
                db.Entry(socialEvent).State = EntityState.Modified;
                db.SaveChanges();
                return Redirect("/SocialEvents?Success=Edit");
            }
            ViewBag.UserUserId = new SelectList(db.AspNetUsers, "Id", "Email", socialEvent.UserUserId);
            ViewBag.POIPlaceId = new SelectList(db.POIs, "PlaceId", "Name", socialEvent.POIPlaceId);
 
            return View(socialEvent);
        }

        /*
         * Name: Social Events Page - Delete()
         * Function implemented to allow User to Delete Social Event 
         * Arguments - Event Id
         * Returns Index Page after Delete  
         */
        [Authorize]
        // GET: SocialEvents/Delete/5
        public ActionResult Delete(int? id)
        {
            var userId = User.Identity.GetUserId();
            var eventLists = db.SocialEvents.Where(s => s.UserUserId == userId).ToList();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SocialEvent socialEvent = db.SocialEvents.Find(id);
            if (socialEvent == null)
            {
                return HttpNotFound();
            }
            bool alreadyExist = eventLists.Contains(socialEvent);
            if (alreadyExist == true)
            {
                return View(socialEvent);
            }
            else
            {
                return RedirectToAction("AccessDenied");
            }
            
        }

        /*
         * Name: Social Events Page - Delete()
         * Function implemented to delete values on DB 
         * Arguments - Event Id
         * Values deleted on DB 
         */
        // POST: SocialEvents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SocialEvent socialEvent = db.SocialEvents.Find(id);
            db.SocialEvents.Remove(socialEvent);
            db.SaveChanges();
            return Redirect("/SocialEvents?Success=Delete");
        }

        /*
         * Name: Social Events Page - Send_Email()
         * Function implemented to allow User to visit Send Email page
         * Arguments - No
         * Return Send Email Page
         */
        [Authorize]
        public ActionResult Send_Email()
        {
            return View(new SendEmailViewModel());
        }

        /*
         * Name: Social Events Page - Share_Facebook()
         * Function implemented to allow User to visit Share on FB page
         * Arguments - No
         * Return Share on FB Page
         */
        public ActionResult Share_Facebook()
        {
            return View();
        }

        /*
         * Name: Social Events Page - Send_Email()
         * Function implemented to allow User to Use SendGrid to Send Email
         * Arguments - Model to get values from Form
         * Return Send Email Page
         */
        [HttpPost]
        public ActionResult Send_Email(SendEmailViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    String toEmail = model.ToEmail;
                    String subject = model.Subject;
                    String contents = model.Contents;


                    EmailSender es = new EmailSender();
                    es.Send(toEmail, subject, contents);

                    ViewBag.Result = "Email has been sent successfully!";

                    ModelState.Clear();

                    return View(new SendEmailViewModel());
                }
                catch
                {
                    return View();
                }
            }

            return View();
        }

        /*
         * Not Part of Functionality
         * Added for Future Scope
         * Send out Bulk Email to Multiple Recepient
         */
        [Authorize(Roles = "Administrator")]
        public ActionResult Send_Bulk_Email()
        {
            return View(new SendEmailViewModelMultipleRecepients());
        }

        /*
         * Not Part of Functionality
         * Added for Future Scope
         * Send out Bulk Email to Multiple Recepient
         */
        [HttpPost]
        public ActionResult Send_Bulk_Email(SendEmailViewModelMultipleRecepients model)
        {
            
            if (ModelState.IsValid)
            {
                try
                {
                    var emails = model.ToEmail;
                    List<String> toEmail = emails.ToList();
                    String subject = model.Subject;
                    String contents = model.Contents;
                    EmailSender es = new EmailSender();
                    es.SendBulkEmail(toEmail, subject, contents);
                    ViewBag.Result = "Emails has been send.";
                    ModelState.Clear();
                    return View(new SendEmailViewModelMultipleRecepients());
                }
                catch
                {
                    return View();
                }
            }

            return View();
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
