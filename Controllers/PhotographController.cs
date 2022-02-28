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

            var x = Session["usertype"];

            if (image != null)
            {
                int ctg = int.Parse(Request.Form["catg"]);
                if (ctg >= 0)
                {
                    var filename = Path.GetFileNameWithoutExtension(image.FileName);
                    var exten = Path.GetExtension(image.FileName);
                    Random rnd = new Random();

                    var myimg = filename + rnd.Next() + exten;

                    /*image.SaveAs(@"C:\Users\Student\Desktop\Charles_Aptech\Digital_photos\PhotoToPrint\" + myimg);*/
                    string _path = Path.Combine(Server.MapPath("~/PhotoToPrint"), myimg);
                    image.SaveAs(_path);

                    data.UserId = (int)Session["id"];
                    data.Photo = myimg;
                    data.category_id = int.Parse(Request.Form["catg"]);

                    Session["phto"] = data.UserId;


                    db.Photographs.Add(data);
                    db.SaveChanges();

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
                else if (ctg <= 0)
                {

                    switch (x)
                    {
                        case 0:
                            return RedirectToAction("Profile", "Home");
                            break;
                        case 1:
                            return View();
                            break;
                    }

                }
            }
            else if (image == null)
            {
                switch (x)
                {
                    case 0:
                        return RedirectToAction("Profile", "Home");
                        break;
                    case 1:
                        return View();
                        break;
                }
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