using StaffPositions.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffPositions.DataAccess.SQL
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
        
        #region Properties
        //Add model to the database
        public DbSet<Developer> Developers { get; set; }
        public DbSet<DeveloperPosition> DeveloperPositions { get; set; }
        #endregion

        #region Methods
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Developer>()
              .HasOptional(e => e.TeamLead)
              .WithMany()
              .HasForeignKey(m => m.TeamLeadID);

            modelBuilder.Entity<Developer>()
              .HasOptional(e => e.Manager)
              .WithMany()
              .HasForeignKey(m => m.ManagerID);
        }
        #endregion
    }
}
