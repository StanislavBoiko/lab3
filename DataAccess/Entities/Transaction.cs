namespace DataAccessLayer.Entities;
public class Transaction
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime DateTime { get; set; }
    
    public Guid? AccountId { get; set; }
    public Account? Account { get; set; }
    
    public Guid? CategoryId { get; set; }
    public Category? Category { get; set; }
}