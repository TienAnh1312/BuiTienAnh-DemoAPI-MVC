using System;
using System.Collections.Generic;

namespace API.Models​;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Avatar { get; set; }

    public decimal? Balance { get; set; }

    public string? Role { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<CommentReport> CommentReports { get; set; } = new List<CommentReport>();

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<History> Histories { get; set; } = new List<History>();

    public virtual ICollection<SearchHistory> SearchHistories { get; set; } = new List<SearchHistory>();

    public virtual ICollection<StoryView> StoryViews { get; set; } = new List<StoryView>();

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

    public virtual ICollection<UserFollowing> UserFollowings { get; set; } = new List<UserFollowing>();

    public virtual ICollection<UserLike> UserLikes { get; set; } = new List<UserLike>();

    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
