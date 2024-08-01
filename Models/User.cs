using System;
using System.Collections.Generic;

namespace financial_manager.Models;

public partial class User
{
    public int Id { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateTime RegistrationDate { get; set; }

    public int MonthlyBudget { get; set; }

    public short BudgetUpdateDay { get; set; }

    public string HashedPassword { get; set; } = null!;

    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
