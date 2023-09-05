using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HairGallery.Models;
using Microsoft.AspNet.Identity;

namespace HairGallery.Controllers
{
    public class HairstylesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Hairstyles
        public ActionResult Index()
        {
            var username = User.Identity.GetUserName();
            var hairstyles = db.Hairstyles.Include(h => h.SalonBuildings).Include(h => h.ServiceTypes);
            return View(hairstyles.ToList().Where(x=>x.CreatedBy == username));
        }
        public ActionResult SalonBooking(int? id)
        {
            var salonId = Convert.ToInt32(Session["BuidlingId"]);
            var hairstyles = db.Hairstyles.Include(h => h.SalonBuildings).Include(h => h.ServiceTypes);
            return View(hairstyles.Where(p => p.SalonBuildingId == salonId && p.ServiceTypeId == id));
        }
        // GET: Hairstyles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hairstyle hairstyle = db.Hairstyles.Find(id);
            if (hairstyle == null)
            {
                return HttpNotFound();
            }
            return View(hairstyle);
        }

        // GET: Hairstyles/Create
        public ActionResult Create()
        {
            var userName = User.Identity.GetUserName();
            ViewBag.SalonBuildingId = new SelectList(db.SalonBuildings.Where(x => x.OwnerEmail == userName), "SalonBuildingId", "SalonName");
            ViewBag.ServiceTypeId = new SelectList(db.ServiceTypes, "ServiceTypeId", "Type");
            return View();
        }

        // POST: Hairstyles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "HairstyleId,SalonBuildingId,ServiceTypeId,HairstyleName,HairstyleDescription,HairstylePicture,HairstylePrice,CreatedBy")] Hairstyle hairstyle, HttpPostedFileBase photoUpload)
        {
            var userName = User.Identity.GetUserName();
            if (ModelState.IsValid)
            {
                byte[] photo = null;
                photo = new byte[photoUpload.ContentLength];
                photoUpload.InputStream.Read(photo, 0, photoUpload.ContentLength);
                hairstyle.HairstylePicture = photo;
                hairstyle.CreatedBy = userName;
                db.Hairstyles.Add(hairstyle);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SalonBuildingId = new SelectList(db.SalonBuildings, "SalonBuildingId", "SalonName", hairstyle.SalonBuildingId);
            ViewBag.ServiceTypeId = new SelectList(db.ServiceTypes, "ServiceTypeId", "Type", hairstyle.ServiceTypeId);
            return View(hairstyle);
        }

        // GET: Hairstyles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hairstyle hairstyle = db.Hairstyles.Find(id);
            if (hairstyle == null)
            {
                return HttpNotFound();
            }
            ViewBag.SalonBuildingId = new SelectList(db.SalonBuildings, "SalonBuildingId", "OwnerEmail", hairstyle.SalonBuildingId);
            ViewBag.ServiceTypeId = new SelectList(db.ServiceTypes, "ServiceTypeId", "Type", hairstyle.ServiceTypeId);
            return View(hairstyle);
        }

        // POST: Hairstyles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "HairstyleId,SalonBuildingId,ServiceTypeId,HairstyleName,HairstyleDescription,HairstylePicture,HairstylePrice,CreatedBy")] Hairstyle hairstyle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hairstyle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SalonBuildingId = new SelectList(db.SalonBuildings, "SalonBuildingId", "OwnerEmail", hairstyle.SalonBuildingId);
            ViewBag.ServiceTypeId = new SelectList(db.ServiceTypes, "ServiceTypeId", "Type", hairstyle.ServiceTypeId);
            return View(hairstyle);
        }

        // GET: Hairstyles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hairstyle hairstyle = db.Hairstyles.Find(id);
            if (hairstyle == null)
            {
                return HttpNotFound();
            }
            return View(hairstyle);
        }

        // POST: Hairstyles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Hairstyle hairstyle = db.Hairstyles.Find(id);
            db.Hairstyles.Remove(hairstyle);
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
