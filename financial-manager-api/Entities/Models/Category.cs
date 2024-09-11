using System;
using System.Collections.Generic;
using financial_manager.Entities.Shared;

namespace financial_manager.Entities.Models;

public partial class Category : BaseModel
{
    public string Title { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public int UserId { get; set; }

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

    public virtual User User { get; set; } = null!;
}
