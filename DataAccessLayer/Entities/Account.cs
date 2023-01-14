using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entities;
using System.Collections.Generic;

public class Account
{
    public int Id { get; set; }
    public decimal CurrentBalance { get; set; }
    public string Name { get; set; }
    
    public ICollection<Transaction>? Transactions { get; set; }


}