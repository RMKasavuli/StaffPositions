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

        #region Properties
        //Add models to the database
        public DbSet<Developer> Developers { get; set; }
        public DbSet<DeveloperPosition> DeveloperPositions { get; set; }
        #endregion

        #region Methods
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Developer>()
              .HasKey(e => e.DeveloperId)
              .HasOptional(e => e.Superior)
              .WithMany()
              .HasForeignKey(m => m.SuperiorID);
        }
        #endregion
    }
}
