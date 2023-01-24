namespace WebLayer.Models;

public class AccountViewModel
{
    public int Id { get; set; }
    public decimal CurrentBalance { get; set; }
    public string Name { get; set; }
  
    public IEnumerable<TransactionViewModel> Incoming { get; set; }
    public IEnumerable<TransactionViewModel> Outgoing { get; set; }
}