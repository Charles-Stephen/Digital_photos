﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Digital_photos
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Digital_Photo_PrintEntities : DbContext
    {
        public Digital_Photo_PrintEntities()
            : base("name=Digital_Photo_PrintEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<category> categories { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<order> orders { get; set; }
        public virtual DbSet<Photograph> Photographs { get; set; }
        public virtual DbSet<Price_Info> Price_Info { get; set; }
        public virtual DbSet<user> users { get; set; }
    }
}
