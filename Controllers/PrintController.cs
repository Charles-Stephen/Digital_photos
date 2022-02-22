using Digital_photos.ViewModal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Dynamic;

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
            return View(db.orders.ToList());
        }



        //==============================================================
        //              CREATE
        //==============================================================
        public ActionResult Create()
        {
            Session["ph"] = db.Photographs.Where(p => p.UserId == (int)Session["id"]);

            var tables = new myuserdetails
            {
                price_Infos = db.Price_Info.ToList(),
                photographs = db.Photographs.ToList()
            };
            return View(tables);
        }
        [HttpPost]
        public ActionResult Create(FormCollection fc)
        {
            order data = new order();

            Random rndNum = new Random();

            data.UserId = (int)Session["id"];
            data.Order_Number = rndNum.Next(1, 100);
            data.Photograph_Id = int.Parse(Request.Form["cars"]);
            data.PriceInfo_Id = int.Parse(Request.Form["selectedsize"]);
            data.Quantity = int.Parse(Request.Form["quantitywant"]);
            data.Total_Price = int.Parse(Request.Form["totalPrice"]);


            return View();
        }

    }
}