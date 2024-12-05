using System;
using System.Collections.Generic;

namespace API.Models​;

public partial class History
{
    public int HistoryId { get; set; }

    public int? UserId { get; set; }

    public int? StoryId { get; set; }

    public int? ChapterId { get; set; }

    public DateTime? ReadAt { get; set; }

    public virtual Chapter? Chapter { get; set; }

    public virtual Story? Story { get; set; }

    public virtual User? User { get; set; }
}
