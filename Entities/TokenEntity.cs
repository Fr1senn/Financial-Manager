using System;
using System.Collections.Generic;

namespace financial_manager.Entities;

public partial class TokenEntity
{
    public int Id { get; set; }

    public string RefreshToken { get; set; } = null!;

    public int UserId { get; set; }

    public DateTime ExpirationDate { get; set; }

    public bool IsRevoked { get; set; }

    public virtual UserEntity User { get; set; } = null!;
}
