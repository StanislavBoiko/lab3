using System.Linq.Expressions;
using DataAccessLayer.DB;
using DataAccessLayer.Entities;

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
        throw new NotImplementedException();
    }

    public void Create(Category item)
    {
        throw new NotImplementedException();
    }

    public void Update(Category item)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }
}