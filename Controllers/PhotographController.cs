using Digital_photos.ViewModal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;
using System.IO;

namespace Digital_photos.Controllers
{
    public class PhotographController : Controller
    {
        // GET: Photograph
        Digital_Photo_PrintEntities db = new Digital_Photo_PrintEntities();



        //============================================================== 
        //              INDEX
        //==============================================================
        public ActionResult Index()
        {
            return View(db.Photographs.ToList());
        }



        //==============================================================
        //              CREATE
        //==============================================================
        public ActionResult Create()
        {
            if (Session["name"] != null)
            {
                var tables = new myuserdetails
                {
                    categories = db.categories.ToList()
                };
                return View(tables);            
            }
            else if (Session["name"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }
        [HttpPost]
        public ActionResult Create(FormCollection fc, HttpPostedFileBase image)
        {
            Photograph data = new Photograph();

            
            if (image != null)
            {
                var filename = Path.GetFileNameWithoutExtension(image.FileName);
                var exten = Path.GetExtension(image.FileName);
                Random rnd = new Random();

                var myimg = filename + rnd.Next() + exten;

                image.SaveAs(Path.Combine(Server.MapPath("~/PhotoToPrint"), myimg));

                data.UserId = (int)Session["id"];
                data.Photo = myimg;
                data.category_id = int.Parse(Request.Form["catg"]);                


                db.Photographs.Add(data);
                db.SaveChanges();
                var x = Session["usertype"];
                switch (x)
                {
                    case 0:
                        return RedirectToAction("Index", "Account");
                        break;
                    case 1:
                        return RedirectToAction("Index", "Home");
                        break;
                }
                return View();
            }
            else if (image == null)
            {
                ViewBag.error = "Please Fill Everything";
                return View();
            }            
            return View();
        }



        //==============================================================
        //              EDIT
        //============================================================== 
        public ActionResult Delete(int? id)
        {
            

            if (Session["name"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Photograph data = db.Photographs.Find(id);
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
            Photograph data = db.Photographs.Find(id);

            db.Photographs.Remove(data);
            db.SaveChanges();


            var x = Session["usertype"];
            switch (x)
            {
                case 0:
                    return RedirectToAction("Index", "Account");
                    break;
                case 1:
                    return RedirectToAction("Index", "Home");
                    break;
            }
            return View();

        }
    }
}