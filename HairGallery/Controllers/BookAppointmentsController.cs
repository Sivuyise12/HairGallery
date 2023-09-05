using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HairGallery.Business_Logic;
using HairGallery.Models;
using Microsoft.AspNet.Identity;

namespace HairGallery.Controllers
{
    public class BookAppointmentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BookAppointments
        public ActionResult Index()
        {
            var userName = User.Identity.GetUserName();
            return View(db.BookAppointments.ToList().Where(x=>x.OwnerEmail == userName));
        }
        public ActionResult MyAppointments()
        {
            var userName = User.Identity.GetUserName();
            return View(db.BookAppointments.ToList().Where(x=>x.CustomerEmail == userName));
        }

        // GET: BookAppointments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookAppointment bookAppointment = db.BookAppointments.Find(id);
            if (bookAppointment == null)
            {
                return HttpNotFound();
            }
            return View(bookAppointment);
        }
        public ActionResult ConfirmAppointment(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookAppointment bookAppointment = db.BookAppointments.Find(id);
            if (bookAppointment == null)
            {
                return HttpNotFound();
            }
            return View(bookAppointment);
        }
        // GET: BookAppointments/Create
        public ActionResult Create(int? id)
        {
            Session["id"] = id;
            return View();
        }

        // POST: BookAppointments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookAppointmentId,serviServiceceType,DateRequestingFor,HairstylePrice,Description,SalonName,HairstyleName,CustomerEmail,DateRequested,TimeSlot")] BookAppointment bookAppointment)
        {
            var id = Convert.ToInt32(Session["id"]);
            if (ModelState.IsValid)
            {
                if (Logic.CheckBooking(bookAppointment) == false)
                {
                    if (Logic.CheckDate(Convert.ToDateTime(bookAppointment.DateRequestingFor)) == false)
                    {
                        bookAppointment.serviceType = Logic.GetServiceType(id);
                        bookAppointment.HairstylePrice = Logic.GetHairstylePrice(id);
                        bookAppointment.Description = Logic.GetDescription(id);
                        bookAppointment.SalonName = Logic.GetSalonName(id);
                        bookAppointment.HairstyleName = Logic.GetHairstyleName(id);
                        bookAppointment.DateRequested = DateTime.Now.Date.ToString();
                        bookAppointment.OwnerEmail = Logic.GetOwnerEmail(id);
                        bookAppointment.CustomerEmail = User.Identity.GetUserName();
                        db.BookAppointments.Add(bookAppointment);
                        db.SaveChanges();
                        return RedirectToAction("ConfirmAppointment", new { id = bookAppointment.BookAppointmentId });
                    }
                    else
                    {
                        ModelState.AddModelError("", "You can not pick a date that has already passed");
                        return View(bookAppointment);
                    }
                }
                else
                {
                    ModelState.AddModelError("", $"This date {bookAppointment.DateRequestingFor} and Time {bookAppointment.TimeSlot} have already been booked. Please select a different Date or Time");
                    return View(bookAppointment);
                }
            }

            return View(bookAppointment);
        }

        // GET: BookAppointments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookAppointment bookAppointment = db.BookAppointments.Find(id);
            if (bookAppointment == null)
            {
                return HttpNotFound();
            }
            return View(bookAppointment);
        }

        // POST: BookAppointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookAppointmentId,serviceType,DateRequestingFor,HairstylePrice,Description,CustomerEmail,DateRequested,TimeSlot")] BookAppointment bookAppointment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bookAppointment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bookAppointment);
        }

        // GET: BookAppointments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookAppointment bookAppointment = db.BookAppointments.Find(id);
            if (bookAppointment == null)
            {
                return HttpNotFound();
            }
            return View(bookAppointment);
        }

        // POST: BookAppointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BookAppointment bookAppointment = db.BookAppointments.Find(id);
            db.BookAppointments.Remove(bookAppointment);
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
