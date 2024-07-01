using CashFlow.Communication.Enums;
using System.ComponentModel.DataAnnotations;

namespace CashFlow.Communication.Requests;


public class RequestExpenseJson
{
    [Required]
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    [Required]
    public DateTime Date { get; set; }

    [Required]
    public decimal Amount { get; set; }

    [Required]
    public PaymentType Type { get; set; }
}
