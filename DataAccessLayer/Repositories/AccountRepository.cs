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

    public IEnumerable<Account> Get(Expression<Func<Account, bool>> filter = null)
    {
        
            IQueryable<Account> query = _context.Accounts;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            query = query.Include(a => a.Incoming)
                .Include(a => a.Outgoing).AsSplitQuery();


            return query;
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