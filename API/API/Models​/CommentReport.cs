using System;
using System.Collections.Generic;

namespace API.Models​;

public partial class CommentReport
{
    public int ReportId { get; set; }

    public int? CommentId { get; set; }

    public int? UserId { get; set; }

    public string? Reason { get; set; }

    public DateTime? ReportAt { get; set; }

    public virtual Comment? Comment { get; set; }

    public virtual User? User { get; set; }
}
