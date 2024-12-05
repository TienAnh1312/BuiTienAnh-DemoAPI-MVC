using System;
using System.Collections.Generic;

namespace testAPI.Models​;

public partial class UserLike
{
    public int UserId { get; set; }

    public int StoryId { get; set; }

    public DateTime? LikedAt { get; set; }

    public virtual Story Story { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
