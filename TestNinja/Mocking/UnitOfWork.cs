using System.Collections.Generic;
using System.Linq;
using TestNinja.Mocking.Interfaces;

namespace TestNinja.Mocking
{
    public class UnitOfWork : IUnitOfWork
    {
        public IQueryable<T> Query<T>()
        {
            return new List<T>().AsQueryable();
        }
    }
}