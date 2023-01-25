using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebLayer.Models;

public class ExpenseViewModel
{
    [Range(0.0, double.MaxValue, ErrorMessage = "Amount must be greater than zero")]

    public decimal Amount { get; set; }

    [DisplayName("From")]
    public int SenderId { get; set; }
    
    public string Category { get; set; }
}