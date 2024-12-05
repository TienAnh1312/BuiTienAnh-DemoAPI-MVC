using System;
using System.Collections.Generic;

namespace API.Models​;

public partial class Story
{
    public int StoryId { get; set; }

    public string Title { get; set; } = null!;

    public string? Author { get; set; }

    public string? Description { get; set; }

    public string? CoverImage { get; set; }

    public string? Status { get; set; }

    public int? TotalChapters { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Chapter> Chapters { get; set; } = new List<Chapter>();

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<History> Histories { get; set; } = new List<History>();

    public virtual ICollection<StoryView> StoryViews { get; set; } = new List<StoryView>();

    public virtual ICollection<UserFollowing> UserFollowings { get; set; } = new List<UserFollowing>();

    public virtual ICollection<UserLike> UserLikes { get; set; } = new List<UserLike>();

    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();
}
