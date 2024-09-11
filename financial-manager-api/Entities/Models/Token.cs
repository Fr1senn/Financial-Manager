using System;
using System.Collections.Generic;
using financial_manager.Entities.Shared;

namespace financial_manager.Entities.Models;

public partial class Token : BaseModel
{
    public string RefreshToken { get; set; } = null!;

    public int UserId { get; set; }

    public DateTime ExpirationDate { get; set; }

    public bool IsRevoked { get; set; }

    public virtual User User { get; set; } = null!;
}
