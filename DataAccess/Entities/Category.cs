namespace DataAccessLayer.Entities;
public class Category
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal TotalMoney { get; set; }
    
    public virtual ICollection<Account>? Accounts { get; set; }
    public virtual ICollection<Transaction>? Transactions { get; set; }
}