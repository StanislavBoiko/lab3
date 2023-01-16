namespace DataAccessLayer.Entities;
public class Transaction
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime DateTime { get; set; } = DateTime.Now;
    
    public Account? Sender { get; set; }
    public int? SenderId { get; set; }

    public Account? Recipient { get; set; }
    public int? RecipientId { get; set; }
    
    public string Category { get; set; }

  
}