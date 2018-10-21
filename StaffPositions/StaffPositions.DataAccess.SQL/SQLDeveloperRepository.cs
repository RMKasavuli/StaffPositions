using StaffPositions.Core.Contracts;
using StaffPositions.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffPositions.DataAccess.SQL
{
    //Developers Repository to link the data in the SQL table to the data context EF Code First
    public class SQLDeveloperRepository<T> : IDeveloperRepository<T> where T : Developer
    {
        internal DataContext context;
        internal DbSet<T> dbSet;
        //constructor to pass the context
        public SQLDeveloperRepository(DataContext context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
        }

        public IQueryable<T> Collection()
        {
            return dbSet;
        }

        public void Commit()
        {
            context.SaveChanges();
        }

        public void Delete(int DeveloperId)
        {
            var t = Find(DeveloperId);
            if (context.Entry(t).State == EntityState.Detached)
                dbSet.Attach(t);
            dbSet.Remove(t);
        }

        public T Find(int DeveloperId)
        {
            return dbSet.Find(DeveloperId);
        }

        public void Insert(T t)
        {
            dbSet.Add(t);
        }

        public void Update(T t)
        {
            //attach the model then simplify it, specify the entry to modify
            dbSet.Attach(t);
            context.Entry(t).State = EntityState.Modified;
        }
    }
}

