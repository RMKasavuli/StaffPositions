using System.Linq;
using StaffPositions.Core.Models;

namespace StaffPositions.Core.Contracts
{
    public interface IPositionRepository<T> where T : BaseEntity
    {
        IQueryable<T> Collection();
        void Commit();
        void Delete(string Id);
        T Find(string Id);
        void Insert(T t);
        void Update(T t);
    }
}