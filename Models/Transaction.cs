using System;
using System.Collections.Generic;

namespace financial_manager.Models;

public partial class Transaction
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string Title { get; set; } = null!;

    public int? CategoryId { get; set; }

    public decimal Significance { get; set; }

    public string TransactionType { get; set; } = null!;

    public DateTime? ExpenseDate { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Category? Category { get; set; }

    public virtual User User { get; set; } = null!;
}
