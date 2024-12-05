using System;
using System.Collections.Generic;

namespace API.Models​;

public partial class Comment
{
    public int CommentId { get; set; }

    public int? StoryId { get; set; }

    public int? UserId { get; set; }

    public string? Content { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<CommentReport> CommentReports { get; set; } = new List<CommentReport>();

    public virtual Story? Story { get; set; }

    public virtual User? User { get; set; }
}
