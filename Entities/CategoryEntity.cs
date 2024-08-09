using System;
using System.Collections.Generic;

namespace financial_manager.Entities;

public partial class CategoryEntity
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public int UserId { get; set; }

    public virtual ICollection<TransactionEntity> Transactions { get; set; } = new List<TransactionEntity>();

    public virtual UserEntity User { get; set; } = null!;
}
