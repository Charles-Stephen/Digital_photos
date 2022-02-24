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


    }
}