using Digital_photos.ViewModal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Data.Entity;
using System.Net;
using System.Dynamic;

namespace Digital_photos.Controllers
{
    public class HomeController : Controller
    {
        Digital_Photo_PrintEntities db = new Digital_Photo_PrintEntities();


        //==============================================================
        //              profile
        //==============================================================
        public ActionResult Profile()
        {
            var tables = new myuserdetails
            {
                users = db.users.ToList(),
                price_Infos = db.Price_Info.ToList(),
                photographs = db.Photographs.ToList(),
                orders = db.orders.ToList()
            };
            return View(tables);
        }


        //==============================================================
        //              INDEX
        //==============================================================
        public ActionResult Index()
        {
            return View();
        }




        //==============================================================
        //              ABOUT
        //==============================================================
        public ActionResult About()
        {
            return View();
        }




        //==============================================================
        //              PORTFOLIO
        //==============================================================
        public ActionResult Portfolio()
        {
            return View();
        }




        //==============================================================
        //              BLOG
        //==============================================================
        public ActionResult Blog()
        {
            return View();
        }




        //==============================================================
        //              CONTACT
        //==============================================================
        public ActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Contact(FormCollection fc)
        {
            Contact contact = new Contact();

            contact.Name = Request.Form["name"];
            contact.Email = Request.Form["email"];
            contact.Phone = Request.Form["phone"];
            contact.Subject_ = Request.Form["subject"];
            contact.Message_ = Request.Form["message"];

            db.Contacts.Add(contact);
            db.SaveChanges();

            return RedirectToAction("Index");
        }



        //==============================================================
        //              ERROR
        //==============================================================
        public ActionResult Error()
        {
            return View();
        }



        //==============================================================
        //              REGISTER
        //==============================================================
        public ActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Registration(FormCollection fc, HttpPostedFileBase image)
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
                    data.User_Type = 1;


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
        //              LOGIN
        //==============================================================

        public ActionResult Login()
        {
            user cust = new user();

            if (Session["name"] == null)
            {
                return View();
            }
            else if (Session["name"] != null)
            {
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
            }
            return View();
        }
        [HttpPost]
        public ActionResult Login(string email, string pass)
        {

            if (Session["name"] == null)
            {

                var mylogin = db.users.FirstOrDefault(a => a.Email == email && a.Pass == pass);

                if (mylogin != null)
                {
                    Session["profile"] = mylogin.User_Profile;
                    Session["usertype"] = mylogin.User_Type;
                    Session["id"] = mylogin.id;
                    Session["name"] = mylogin.First_Name + " " + mylogin.Last_Name;

                    Session["name1"] = mylogin.First_Name;
                    Session["name2"] = mylogin.Last_Name;
                    Session["email"] = mylogin.Email;


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

                }
                else
                {
                    ViewBag.emailerror = "Invalid Email Password";
                    return View();
                }
            }
            else
            {
                return View();
            }

            return View();
        }



        //=================================================================================================
        // *************************** FRONTEND LAYOUT ****************************************************
        //=================================================================================================
        public ActionResult Frontend_Layout()
        {
            Session["ph"] = db.Photographs.Where(p => p.UserId == (int)Session["id"]);            

            var tables = new myuserdetails
            {
                price_Infos = db.Price_Info.ToList(),
                photographs = db.Photographs.ToList()
            };
            return View(tables);
        }

    }
}