using System;
using System.Collections.Generic;
using financial_manager.Entities.Shared;

namespace financial_manager.Entities.Models;

public partial class User : BaseModel
{
    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateTime RegistrationDate { get; set; }

    public int MonthlyBudget { get; set; }

    public short BudgetUpdateDay { get; set; }

    public string HashedPassword { get; set; } = null!;

    public byte[] PasswordSalt { get; set; } = null!;

    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();

    public virtual ICollection<Token> Tokens { get; set; } = new List<Token>();

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
