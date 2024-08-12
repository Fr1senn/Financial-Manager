using System;
using System.Collections.Generic;

namespace financial_manager.Entities;

public partial class TransactionEntity
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string Title { get; set; } = null!;

    public int? CategoryId { get; set; }

    public decimal Significance { get; set; }

    public string TransactionType { get; set; } = null!;

    public DateTime ExpenseDate { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual CategoryEntity? Category { get; set; }

    public virtual UserEntity User { get; set; } = null!;
}
