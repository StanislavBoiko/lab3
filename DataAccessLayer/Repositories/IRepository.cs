using System.Linq.Expressions;

namespace DataAccessLayer.Repositories;

public interface IRepository<T> where T : class
{
    T GetById(Guid id);
    IEnumerable<T> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>,
        IOrderedQueryable<T>> orderBy = null,
        string includeProperties = "");
    void Create(T item);
    void Update(T item);
    void Delete(Guid id);
}