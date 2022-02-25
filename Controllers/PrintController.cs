using Digital_photos.ViewModal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Dynamic;
using System.Net;
using System.Data.Entity;
using System.IO;

namespace Digital_photos.Controllers
{
    public class PrintController : Controller
    {
        Digital_Photo_PrintEntities db = new Digital_Photo_PrintEntities();
        // GET: Print



        //==============================================================
        //              INDEX
        //==============================================================
        public ActionResult Index()
        {
            if (Session["name"] != null)
            {
                return View(db.orders.ToList());                
            }
            else if (Session["name"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }



        //==============================================================
        //              CREATE
        //==============================================================
        public ActionResult Create()
        {
            if (Session["name"] != null)
            {
                Session["ph"] = db.Photographs.Where(p => p.UserId == (int)Session["id"]);

                var tables = new myuserdetails
                {
                    price_Infos = db.Price_Info.ToList(),
                    photographs = db.Photographs.ToList()
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
        public ActionResult Create(FormCollection fc)
        {
            order data = new order();

            var myuser = (int)Session["id"];
            int id = myuser;

            user data2 = db.users.Find(id);

            Random rndNum = new Random();


            var mycard = Request.Form["creditNo"];
            var mycredit = db.users.FirstOrDefault(a => a.Credit_Card == mycard && a.id == myuser);

            if (mycard != null)
            {
                var myorderid = rndNum.Next(1, 100);


                data.UserId = myuser;
                data.Order_Number = myorderid;
                data.Photograph_Id = int.Parse(Request.Form["cars"]);
                data.PriceInfo_Id = int.Parse(Request.Form["selectedsize"]);
                data.Quantity = int.Parse(Request.Form["quantitywant"]);
                data.Total_Price = int.Parse(Request.Form["totalPrice"]);
                data.Credit_No = Request.Form["creditNo"];
                data2.Order_id = myorderid;

                db.orders.Add(data);

                db.Entry(data2).State = EntityState.Modified;
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
            else if (mycard == null)
            {
                ViewBag.er = "Invalid Credit Card";
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

                order data = db.orders.Find(id);

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
        public ActionResult Edit(int id, FormCollection fc)
        {
            order data = db.orders.Find(id);

            data.PriceInfo_Id = int.Parse(Request.Form["selectedsize"]);
            data.Quantity = int.Parse(Request.Form["quantitywant"]);

            db.Entry(data).State = EntityState.Modified;
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
                order data = db.orders.Find(id);
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
            order data = db.orders.Find(id);

            db.orders.Remove(data);
            db.SaveChanges();
            
            return View();
        }
    }
}