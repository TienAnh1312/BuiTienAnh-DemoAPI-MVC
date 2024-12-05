using System;
using System.Collections.Generic;

namespace testAPI.Models​;

public partial class SearchHistory
{
    public int SearchId { get; set; }

    public int? UserId { get; set; }

    public string? SearchTerm { get; set; }

    public DateTime? SearchAt { get; set; }

    public virtual User? User { get; set; }
}
