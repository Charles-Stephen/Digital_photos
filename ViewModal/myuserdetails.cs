using Digital_photos.ViewModal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Digital_photos.ViewModal
{
    public class myuserdetails
    {
        public IEnumerable<user> users { get; set; }
        public IEnumerable<Price_Info> price_Infos { get; set; }
        public IEnumerable<Contact> contacts { get; set; }
        public IEnumerable<Photograph> photographs { get; set; }
        public IEnumerable<order> orders { get; set; }
        public IEnumerable<category> categories { get; set; }
    }
}