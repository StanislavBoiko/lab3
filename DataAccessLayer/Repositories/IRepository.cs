using System.Linq.Expressions;

namespace DataAccessLayer.Repositories;

public interface IRepository<T> where T : class
{
    T GetById(int id);
    IEnumerable<T> Get(Expression<Func<T, bool>> filter = null);
    void Create(T item);
    void Update(T item);
    void Delete(int id);
}