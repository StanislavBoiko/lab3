using DataAccessLayer.DB;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccessLayer.Repositories;

public class GenericRepository<T> : IRepository<T> where T : class
{

    internal WalletContext _context;
    internal DbSet<T> _dbSet; 

    public GenericRepository(WalletContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public T GetById(int id)
    {
        return _dbSet.Find(id);
    }

    public IEnumerable<T> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>,
        IOrderedQueryable<T>> orderBy = null,
        string includeProperties = "")
    {
        IQueryable<T> query = _dbSet;

        if (filter != null)
        {
            query = query.Where(filter);
        }

        foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty);
        }

        if (orderBy != null)
        {
            return orderBy(query).ToList();
        }
        else
        {
            return query;
        }
    }

    public void Create(T item)
    {
        _dbSet.Add(item);    
    }

    public void Update(T item)
    {
        _dbSet.Entry(item).State= EntityState.Modified;
    }

    public void Delete(int id)
    {
        T item = _dbSet.Find(id);
        if(item != null)
        {
            _dbSet.Remove(item);
        }
    }
}