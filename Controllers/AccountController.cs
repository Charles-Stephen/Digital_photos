using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Net;
using System.Data.Entity;

namespace Photo.Controllers
{
    public class AccountController : Controller
    {
        Digital_Photo_PrintEntities db = new Digital_Photo_PrintEntities();




        //==============================================================
        //              INDEX
        //==============================================================
        public ActionResult Index()
        {
            if (Session["name"] != null)
            {
                return View();
            }
            else if (Session["name"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }


        //==============================================================
        //              REGISTER
        //==============================================================
        public ActionResult Reg()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Reg(FormCollection fc, HttpPostedFileBase image)
        {
            user data = new user();

            var eemail = Request.Form["email"];
            var epass = Request.Form["pass"];
            var mylogin = db.users.FirstOrDefault(a => a.Email == eemail && a.Pass == epass);


            if (mylogin == null)
            {
                if (image != null)
                {
                    var filename = Path.GetFileNameWithoutExtension(image.FileName);
                    var exten = Path.GetExtension(image.FileName);
                    Random rnd = new Random();

                    var myimg = filename + rnd.Next() + exten;

                    image.SaveAs(@"D:\E-Project\Photo\Profile_Img\" + myimg);

                    data.First_Name = Request.Form["firstname"];
                    data.Last_Name = Request.Form["lastname"];
                    data.Email = Request.Form["email"];
                    data.Pass = Request.Form["pass"];
                    data.Date_Of_Birth = Request.Form["birth"];
                    data.Gender = Request.Form["gender"];
                    data.Phone = Request.Form["phone"];
                    data.User_Profile = myimg;
                    data.User_Type = 0;


                    db.users.Add(data);
                    db.SaveChanges();
                    return RedirectToAction("Login", "Home");
                }
                else if (image == null)
                {
                    ViewBag.error = "Please Fill Everything";
                    return View();
                }
            }
            else
            {
                ViewBag.error = "Email Already Exists";
                return View();
            }
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

                user data = db.users.Find(id);

                if (data == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.NotFound);
                }
                else
                {
                    return View(data);
                }
                return View(data);
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
            user data = db.users.Find(id);


            var filename = Path.GetFileNameWithoutExtension(image.FileName);
            var exten = Path.GetExtension(image.FileName);
            Random rnd = new Random();

            var myimg = filename + rnd.Next() + exten;

            image.SaveAs(@"D:\E-Project\2nd_Sem_E-Project 2022\Printing_Photo_Online\profile_img\" + myimg);

            data.id = int.Parse(Request.Form["id"]);
            data.First_Name = Request.Form["firstname"];
            data.Last_Name = Request.Form["lastname"];
            data.Email = Request.Form["email"];
            data.Pass = Request.Form["pass"];
            data.Date_Of_Birth = Request.Form["birth"];
            data.Gender = Request.Form["gender"];
            data.Phone = Request.Form["phone"];
            data.User_Profile = myimg;

            db.Entry(data).State = EntityState.Modified;
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




        //==============================================================
        //              DELETE
        //==============================================================

        public ActionResult Delete(int? id)
        {
            ViewBag.yourtype = Session["usertype"];

            if (Session["name"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                user data = db.users.Find(id);
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
            user data = db.users.Find(id);

            db.users.Remove(data);
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




        //==============================================================
        //              LOGOUT
        //==============================================================

        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Login", "Home");
        }


    }
}