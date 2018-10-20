using System.Linq;
using StaffPositions.Core.Models;

namespace StaffPositions.Core.Contracts
{
    public interface IDeveloperRepository<T> where T : Developer
    {
        IQueryable<T> Collection();
        void Commit();
        void Delete(int DeveloperId);
        T Find(int DeveloperId);
        void Insert(T t);
        void Update(T t);
    }
}
