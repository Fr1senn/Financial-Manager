using System;
using System.Collections.Generic;

namespace financial_manager.Entities;

public partial class UserEntity
{
    public int Id { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateTime RegistrationDate { get; set; }

    public int MonthlyBudget { get; set; }

    public short BudgetUpdateDay { get; set; }

    public string HashedPassword { get; set; } = null!;

    public byte[]? PasswordSalt { get; set; }

    public virtual ICollection<CategoryEntity> Categories { get; set; } = new List<CategoryEntity>();

    public virtual ICollection<TransactionEntity> Transactions { get; set; } = new List<TransactionEntity>();
}
