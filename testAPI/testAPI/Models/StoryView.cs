using System;
using System.Collections.Generic;

namespace testAPI.Models​;

public partial class StoryView
{
    public int ViewId { get; set; }

    public int? StoryId { get; set; }

    public int? UserId { get; set; }

    public DateTime? ViewedAt { get; set; }

    public string? IpAddress { get; set; }

    public virtual Story? Story { get; set; }

    public virtual User? User { get; set; }
}
