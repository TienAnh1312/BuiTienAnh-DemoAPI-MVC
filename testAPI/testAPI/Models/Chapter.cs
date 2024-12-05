using System;
using System.Collections.Generic;

namespace testAPI.Models​;

public partial class Chapter
{
    public int ChapterId { get; set; }

    public int? StoryId { get; set; }

    public string? Title { get; set; }

    public string? Content { get; set; }

    public decimal? Price { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<History> Histories { get; set; } = new List<History>();

    public virtual Story? Story { get; set; }
}
