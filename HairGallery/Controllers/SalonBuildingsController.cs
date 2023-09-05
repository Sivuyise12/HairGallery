using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using HairGallery.Models;
using Microsoft.AspNet.Identity;

namespace HairGallery.Controllers
{
    public class SalonBuildingsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SalonBuildings
        public ActionResult Index()
        {
            var username = User.Identity.GetUserName();
            return View(db.SalonBuildings.ToList().Where(x=>x.OwnerEmail == username));
        }
        public ActionResult Salons()
        {
            return View(db.SalonBuildings.ToList());
        }
        // GET: SalonBuildings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalonBuilding salonBuilding = db.SalonBuildings.Find(id);
            if (salonBuilding == null)
            {
                return HttpNotFound();
            }
            return View(salonBuilding);
        }

        // GET: SalonBuildings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SalonBuildings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SalonBuildingId,OwnerEmail,SalonName,Status,BuildingDescription,BuildingPic,Address")] SalonBuilding salonBuilding, HttpPostedFileBase photoUpload)
        {
            var email = User.Identity.GetUserName();
            if (ModelState.IsValid)
            {
                var userName = User.Identity.GetUserName();
                byte[] photo = null;
                photo = new byte[photoUpload.ContentLength];
                photoUpload.InputStream.Read(photo, 0, photoUpload.ContentLength);
                salonBuilding.BuildingPic = photo;
                salonBuilding.OwnerEmail = userName;

                db.SalonBuildings.Add(salonBuilding);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(salonBuilding);
        }

        // GET: SalonBuildings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalonBuilding salonBuilding = db.SalonBuildings.Find(id);
            if (salonBuilding == null)
            {
                return HttpNotFound();
            }
            return View(salonBuilding);
        }

        // POST: SalonBuildings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SalonBuildingId,OwnerEmail,SalonName,Status,BuildingDescription,BuildingPic,Address")] SalonBuilding salonBuilding)
        {
            if (ModelState.IsValid)
            {
                db.Entry(salonBuilding).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(salonBuilding);
        }

        // GET: SalonBuildings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalonBuilding salonBuilding = db.SalonBuildings.Find(id);
            if (salonBuilding == null)
            {
                return HttpNotFound();
            }
            return View(salonBuilding);
        }

        // POST: SalonBuildings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SalonBuilding salonBuilding = db.SalonBuildings.Find(id);
            db.SalonBuildings.Remove(salonBuilding);
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
