using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entities;
using System.Collections.Generic;

public class Account
{
    public int Id { get; set; }

    public decimal CurrentBalance
    {
        get
        {
            decimal total = 0;
            foreach (Transaction t in Incoming)
            {
                total += t.Amount;
            }

            foreach (Transaction t in Outgoing)
            {
                total -= t.Amount;
            }

            return total;
        }
    }

    public string Name { get; set; }
  
    public  IEnumerable<Transaction> Incoming { get; set; }
    public  IEnumerable<Transaction> Outgoing { get; set; }

}