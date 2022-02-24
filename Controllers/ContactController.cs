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
    public class ContactController : Controller
    {
        Digital_Photo_PrintEntities db = new Digital_Photo_PrintEntities();
        // GET: Contact


        //==============================================================
        //              INDEX
        //==============================================================
        public ActionResult Index()
        {
            return View(db.Contacts.ToList());
        }


        //==============================================================
        //              CREATE
        //==============================================================
        public ActionResult Create()
        {
            return View();
        }


        //==============================================================
        //              EDIT
        //==============================================================
        public ActionResult Edit(int? id)
        {
            if (Session["name"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                Contact contact = db.Contacts.Find(id);

                if (contact == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.NotFound);
                }
                else
                {
                    return View(contact);
                }
                return View(contact);
            }
            else if (Session["name"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            return View();
        }
        [HttpPost]
        public ActionResult Edit(int id, FormCollection fc, HttpPostedFileBase image)
        {
            Contact contact = new Contact();

            contact.id = int.Parse(Request.Form["id"]);
            contact.Name = Request.Form["name"];
            contact.Email = Request.Form["email"];
            contact.Phone = Request.Form["phone"];
            contact.Subject_ = Request.Form["subject"];
            contact.Message_ = Request.Form["message"];

            db.Entry(contact).State = EntityState.Modified;
            db.SaveChanges();
            
            return View();
        }



        //==============================================================
        //              DELETE
        //==============================================================
        public ActionResult Delete(int? id)
        {
            

            if (Session["name"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Contact data = db.Contacts.Find(id);
                if (data == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.NotFound);
                }
                else
                {
                    return View(data);
                }
            }
            else if (Session["name"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }
        [HttpPost, ActionName("Delete")]

        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Contact data = db.Contacts.Find(id);

            db.Contacts.Remove(data);
            db.SaveChanges();
            
            return View();

        }

    }
}