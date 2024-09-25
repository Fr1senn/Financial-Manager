using System;
using System.Collections.Generic;
using financial_manager.Entities.Shared;

namespace financial_manager.Entities.Models;

public partial class Transaction : BaseModel
{
    public int UserId { get; set; }

    public string Title { get; set; } = null!;

    public int? CategoryId { get; set; }

    public decimal Significance { get; set; }

    public string TransactionType { get; set; } = null!;

    public DateTime? ExpenseDate { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Category? Category { get; set; }

    public virtual User User { get; set; } = null!;
}
