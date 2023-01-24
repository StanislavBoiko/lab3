namespace WebLayer.Models;

public class TransactionViewModel
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime DateTime { get; set; }
    
    public AccountViewModel? Sender { get; set; }

    public AccountViewModel? Recipient { get; set; }
    
    public string Category { get; set; }
}