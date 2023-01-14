using System.Linq.Expressions;

namespace DataAccessLayer.Repositories;

public class GenericRepository<T> : IRepository<T> where T : class
{
    public T GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<T> Get(Expression<Func<T, bool>> filter = null)
    {
        throw new NotImplementedException();
    }

    public void Create(T item)
    {
        throw new NotImplementedException();    
    }

    public void Update(T item)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }
}