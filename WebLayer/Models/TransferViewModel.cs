using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebLayer.Models;

public class TransferViewModel
{
    [DisplayName("Sender")]
    public int SenderId { get; set; }
    [DisplayName("Recipient")]
    public int RecipientId { get; set; }
    [Range(0.0, double.MaxValue, ErrorMessage = "Amount must be greater than zero")]
    public decimal Amount { get; set; }
}