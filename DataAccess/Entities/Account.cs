using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entities;
using System.Collections.Generic;

public class Account
{
    public Guid Id { get; set; }
    public decimal CurrentBalance { get; set; }
    public string Name { get; set; }
    
    public virtual ICollection<Transaction>? Transactions { get; set; }
    public virtual ICollection<Category>? Categories { get; set; }
}