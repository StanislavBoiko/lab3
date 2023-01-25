using System.Linq.Expressions;
using DataAccessLayer.DB;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories;

public class TransactionRepository : IRepository<Transaction>
{
    private WalletContext _context;

    public TransactionRepository(WalletContext context)
    {
        _context = context;
    }

    public Transaction GetById(int id)
    {
        return _context.Transactions.Find(id);
    }

    public IEnumerable<Transaction> Get(Expression<Func<Transaction, bool>> filter = null)
    {
        IQueryable<Transaction> query = _context.Transactions;   
        if (filter != null)
        {
            query = query.Where(filter);
        }
       
               
        query = query.Include(t => t.Recipient).Include(t => t.Sender).AsSplitQuery();
               
       
              
        return query;
               
    }

    public void Create(Transaction item)
    {
        _context.Transactions.Add(item);
    }

    public void Update(Transaction item)
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