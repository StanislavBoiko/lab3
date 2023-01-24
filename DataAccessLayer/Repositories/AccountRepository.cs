using System.Linq.Expressions;
using DataAccessLayer.DB;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories;

public class AccountRepository : IRepository<Account>
{
    private WalletContext _context;

    public AccountRepository(WalletContext context)
    {
        _context = context;
    }

    public Account GetById(int id)
    {
        return _context.Accounts.Find(id);
    }

    public IEnumerable<Account> Get(Expression<Func<Account, bool>> filter = null,
        Func<IQueryable<Account>, IOrderedQueryable<Account>> orderBy = null, string includeProperties = "")
    {
        
            IQueryable<Account> query = _context.Accounts;

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

    public void Create(Account item)
    {
        _context.Accounts.Add(item);
    }

    public void Update(Account item)
    {
        _context.Update(item);
    }

    public void Delete(int id)
    {
        Account item = _context.Accounts.Find(id);
        if (item != null)
        {
            _context.Remove(item);
        }
    }
}