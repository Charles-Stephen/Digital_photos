using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Digital_photos;

namespace Digital_photos.Controllers
{
    public class Price_InfoController : Controller
    {
        private Digital_Photo_PrintEntities db = new Digital_Photo_PrintEntities();

        // GET: Price_Info
        public ActionResult Index()
        {
            return View(db.Price_Info.ToList());
        }

        

        // GET: Price_Info/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Price_Info/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Size,Price,Sale_Price")] Price_Info price_Info)
        {
            if (ModelState.IsValid)
            {
                db.Price_Info.Add(price_Info);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(price_Info);
        }

        // GET: Price_Info/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Price_Info price_Info = db.Price_Info.Find(id);
            if (price_Info == null)
            {
                return HttpNotFound();
            }
            return View(price_Info);
        }

        // POST: Price_Info/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Size,Price,Sale_Price")] Price_Info price_Info)
        {
            if (ModelState.IsValid)
            {
                db.Entry(price_Info).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(price_Info);
        }

        // GET: Price_Info/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Price_Info price_Info = db.Price_Info.Find(id);
            if (price_Info == null)
            {
                return HttpNotFound();
            }
            return View(price_Info);
        }

        // POST: Price_Info/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Price_Info price_Info = db.Price_Info.Find(id);
            db.Price_Info.Remove(price_Info);
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
