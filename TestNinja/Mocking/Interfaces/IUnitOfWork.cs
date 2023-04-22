using System.Linq;

namespace TestNinja.Mocking.Interfaces
{
    public interface IUnitOfWork
    {
        IQueryable<T> Query<T>();
    }
}