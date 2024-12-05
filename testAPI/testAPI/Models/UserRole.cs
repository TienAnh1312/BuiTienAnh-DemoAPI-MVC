using System;
using System.Collections.Generic;

namespace testAPI.Models​;

public partial class UserRole
{
    public int UserId { get; set; }

    public string Role { get; set; } = null!;

    public DateTime? AssignedAt { get; set; }

    public virtual User User { get; set; } = null!;
}
