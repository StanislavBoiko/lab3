namespace DataAccessLayer.Entities;
public class Transaction
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime DateTime { get; set; } = DateTime.Now;
    
    public virtual Account? Sender { get; set; }
    public int? SenderId { get; set; }

    public virtual Account? Recipient { get; set; }
    public int? RecipientId { get; set; }
    
    public int CategoryId { get; set; }
    public Category Category { get; set; }

  
}