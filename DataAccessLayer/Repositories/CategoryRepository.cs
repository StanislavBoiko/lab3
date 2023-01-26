using System.Linq.Expressions;
using DataAccessLayer.DB;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories;

public class CategoryRepository : IRepository<Category>
{

    private WalletContext _context;

    public CategoryRepository(WalletContext context)
    {
        _context = context;
    }

    public Category GetById(int id)
    {
        return _context.Categories.Find();
    }

    public IEnumerable<Category> Get(Expression<Func<Category, bool>> filter = null)
    {
        IQueryable<Category> query = _context.Categories;

        if (filter != null)
        {
            query = query.Where(filter);
        }

        query = query.Include(c => c.Transactions);


        return query;
    }

    public void Create(Category item)
    {
        _context.Categories.Add(item);
    }

    public void Update(Category item)
    {
        _context.Categories.Update(item);
    }

    public void Delete(int id)
    {
        Category category = _context.Categories.FirstOrDefault(c => c.Id == id);

        if (category != null)
        {
            _context.Categories.Remove(category);
        }
    }
    
    
    
}