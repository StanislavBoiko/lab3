using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebLayer.Models;

public class TransactionViewModel
{
    [Range(0.0, double.MaxValue, ErrorMessage = "Amount must be greater than zero")]

    public decimal Amount { get; set; }
    

    public string Category { get; set; }

    public DateTime DateTime { get; set; } = DateTime.UtcNow;
}