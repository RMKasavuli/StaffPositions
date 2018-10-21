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
    //Positions Repository to link the data in the SQL table to the data context EF Code First
    public class SQLPositionRepository<T> : IPositionRepository<T>  where T : BaseEntity
    {
        internal DataContext context;
        internal DbSet<T> dbSet;

        //constructor to pass the context
        public SQLPositionRepository(DataContext context)
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

        public void Delete(string Id)
        {
            var t = Find(Id);
            if (context.Entry(t).State == EntityState.Detached)
                dbSet.Attach(t);
            dbSet.Remove(t);
        }

        public T Find(string Id)
        {
            return dbSet.Find(Id);
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
