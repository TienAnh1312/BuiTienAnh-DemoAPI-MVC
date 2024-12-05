using System;
using System.Collections.Generic;

namespace API.Models​;

public partial class InappropriateWord
{
    public int WordId { get; set; }

    public string Word { get; set; } = null!;

    public DateTime? AddedAt { get; set; }
}
