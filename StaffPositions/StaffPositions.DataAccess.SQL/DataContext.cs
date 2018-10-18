﻿using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.SQL
{
    public class DataContext: DbContext
    {
        public DataContext ()
            : base("DefaultConnection")//to look in webconfig for the DefaultConnection string 
        {
            
        }

        //Add models to the database
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategoriess { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
        public DbSet<Customer> Customers { get; set; }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    //.HasOptional(e => e.Manager)
        //    //       . WithMany()
        //    modelBuilder.Entity<Employee>()
        //        .
        //      .HasForeignKey(m => m.ManagerID)
              
        //      ;
        //}
    }
}
