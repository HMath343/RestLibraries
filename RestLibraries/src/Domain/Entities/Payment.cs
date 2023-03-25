namespace RestLibraries.Domain.Entities;

using System;

public class Payment
{
    public int Id { get; set; }
    public string Reference { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; }
    public string TargetCurrency { get; set; }
    public decimal Rate { get; set; }
    public DateTime PaymentDate { get; set; }

    public Payment()
    {
        PaymentDate = DateTime.UtcNow;
    }

    public decimal GetAmount()
    {
        return Math.Round(Amount * Rate, 2);
    }
}