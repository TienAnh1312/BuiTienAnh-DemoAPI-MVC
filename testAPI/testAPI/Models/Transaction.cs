using System;
using System.Collections.Generic;

namespace testAPI.Models​;

public partial class Transaction
{
    public int TransactionId { get; set; }

    public int? UserId { get; set; }

    public string? TransactionType { get; set; }

    public decimal? Amount { get; set; }

    public DateTime? TransactionDate { get; set; }

    public virtual User? User { get; set; }
}
