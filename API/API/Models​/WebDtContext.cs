using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace API.Models​;

public partial class WebDtContext : DbContext
{
    public WebDtContext()
    {
    }

    public WebDtContext(DbContextOptions<WebDtContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Chapter> Chapters { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<CommentReport> CommentReports { get; set; }

    public virtual DbSet<History> Histories { get; set; }

    public virtual DbSet<InappropriateWord> InappropriateWords { get; set; }

    public virtual DbSet<SearchHistory> SearchHistories { get; set; }

    public virtual DbSet<Story> Stories { get; set; }

    public virtual DbSet<StoryView> StoryViews { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserFollowing> UserFollowings { get; set; }

    public virtual DbSet<UserLike> UserLikes { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-53K42V9;Database=WebDT;uid=sa;pwd=123456;MultipleActiveResultSets=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Categori__D54EE9B4E796E0B7");

            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Chapter>(entity =>
        {
            entity.HasKey(e => e.ChapterId).HasName("PK__Chapters__745EFE87F6719ABA");

            entity.Property(e => e.ChapterId).HasColumnName("chapter_id");
            entity.Property(e => e.Content)
                .HasColumnType("text")
                .HasColumnName("content");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Price)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("price");
            entity.Property(e => e.StoryId).HasColumnName("story_id");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("title");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Story).WithMany(p => p.Chapters)
                .HasForeignKey(d => d.StoryId)
                .HasConstraintName("FK_Chapter_Story");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("PK__Comments__E7957687FE5D8C00");

            entity.Property(e => e.CommentId).HasColumnName("comment_id");
            entity.Property(e => e.Content)
                .HasColumnType("text")
                .HasColumnName("content");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.StoryId).HasColumnName("story_id");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Story).WithMany(p => p.Comments)
                .HasForeignKey(d => d.StoryId)
                .HasConstraintName("FK_Comments_Story");

            entity.HasOne(d => d.User).WithMany(p => p.Comments)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Comments_User");
        });

        modelBuilder.Entity<CommentReport>(entity =>
        {
            entity.HasKey(e => e.ReportId).HasName("PK__Comment___779B7C58FC17D18A");

            entity.ToTable("Comment_Reports");

            entity.Property(e => e.ReportId).HasColumnName("report_id");
            entity.Property(e => e.CommentId).HasColumnName("comment_id");
            entity.Property(e => e.Reason)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("reason");
            entity.Property(e => e.ReportAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("report_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Comment).WithMany(p => p.CommentReports)
                .HasForeignKey(d => d.CommentId)
                .HasConstraintName("FK_Report_Comment");

            entity.HasOne(d => d.User).WithMany(p => p.CommentReports)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Report_User");
        });

        modelBuilder.Entity<History>(entity =>
        {
            entity.HasKey(e => e.HistoryId).HasName("PK__History__096AA2E9E5CC56DB");

            entity.ToTable("History");

            entity.Property(e => e.HistoryId).HasColumnName("history_id");
            entity.Property(e => e.ChapterId).HasColumnName("chapter_id");
            entity.Property(e => e.ReadAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("read_at");
            entity.Property(e => e.StoryId).HasColumnName("story_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Chapter).WithMany(p => p.Histories)
                .HasForeignKey(d => d.ChapterId)
                .HasConstraintName("FK_History_Chapter");

            entity.HasOne(d => d.Story).WithMany(p => p.Histories)
                .HasForeignKey(d => d.StoryId)
                .HasConstraintName("FK_History_Story");

            entity.HasOne(d => d.User).WithMany(p => p.Histories)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_History_User");
        });

        modelBuilder.Entity<InappropriateWord>(entity =>
        {
            entity.HasKey(e => e.WordId).HasName("PK__Inapprop__7FFA1D40E69254B9");

            entity.ToTable("Inappropriate_Words");

            entity.Property(e => e.WordId).HasColumnName("word_id");
            entity.Property(e => e.AddedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("added_at");
            entity.Property(e => e.Word)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("word");
        });

        modelBuilder.Entity<SearchHistory>(entity =>
        {
            entity.HasKey(e => e.SearchId).HasName("PK__Search_H__B302268DF6DCA798");

            entity.ToTable("Search_History");

            entity.Property(e => e.SearchId).HasColumnName("search_id");
            entity.Property(e => e.SearchAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("search_at");
            entity.Property(e => e.SearchTerm)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("search_term");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.SearchHistories)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_SearchHistory_User");
        });

        modelBuilder.Entity<Story>(entity =>
        {
            entity.HasKey(e => e.StoryId).HasName("PK__Stories__66339C562D535C73");

            entity.Property(e => e.StoryId).HasColumnName("story_id");
            entity.Property(e => e.Author)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("author");
            entity.Property(e => e.CoverImage)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("cover_image");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("ongoing")
                .HasColumnName("status");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("title");
            entity.Property(e => e.TotalChapters)
                .HasDefaultValue(0)
                .HasColumnName("total_chapters");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasMany(d => d.Categories).WithMany(p => p.Stories)
                .UsingEntity<Dictionary<string, object>>(
                    "StoryCategory",
                    r => r.HasOne<Category>().WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Category_Story"),
                    l => l.HasOne<Story>().WithMany()
                        .HasForeignKey("StoryId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Story_Category"),
                    j =>
                    {
                        j.HasKey("StoryId", "CategoryId").HasName("PK__Story_Ca__3B6772CD5BA07015");
                        j.ToTable("Story_Categories");
                        j.IndexerProperty<int>("StoryId").HasColumnName("story_id");
                        j.IndexerProperty<int>("CategoryId").HasColumnName("category_id");
                    });
        });

        modelBuilder.Entity<StoryView>(entity =>
        {
            entity.HasKey(e => e.ViewId).HasName("PK__Story_Vi__B5A34EE226EB1D27");

            entity.ToTable("Story_Views");

            entity.Property(e => e.ViewId).HasColumnName("view_id");
            entity.Property(e => e.IpAddress)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("ip_address");
            entity.Property(e => e.StoryId).HasColumnName("story_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.ViewedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("viewed_at");

            entity.HasOne(d => d.Story).WithMany(p => p.StoryViews)
                .HasForeignKey(d => d.StoryId)
                .HasConstraintName("FK_Views_Story");

            entity.HasOne(d => d.User).WithMany(p => p.StoryViews)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Views_User");
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.TransactionId).HasName("PK__Transact__85C600AF11BF239A");

            entity.Property(e => e.TransactionId).HasColumnName("transaction_id");
            entity.Property(e => e.Amount)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("amount");
            entity.Property(e => e.TransactionDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("transaction_date");
            entity.Property(e => e.TransactionType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("transaction_type");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Transactions_User");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__B9BE370F2B27AFBB");

            entity.HasIndex(e => e.Email, "UQ__Users__AB6E6164135AA095").IsUnique();

            entity.HasIndex(e => e.Username, "UQ__Users__F3DBC5720E88C559").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Avatar)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("avatar");
            entity.Property(e => e.Balance)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("balance");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("role");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("username");
        });

        modelBuilder.Entity<UserFollowing>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.StoryId }).HasName("PK__User_Fol__CFDD0ECA193ED2F0");

            entity.ToTable("User_Following");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.StoryId).HasColumnName("story_id");
            entity.Property(e => e.FollowedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("followed_at");

            entity.HasOne(d => d.Story).WithMany(p => p.UserFollowings)
                .HasForeignKey(d => d.StoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Following_Story");

            entity.HasOne(d => d.User).WithMany(p => p.UserFollowings)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Following_User");
        });

        modelBuilder.Entity<UserLike>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.StoryId }).HasName("PK__User_Lik__CFDD0ECA8250EA20");

            entity.ToTable("User_Likes");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.StoryId).HasColumnName("story_id");
            entity.Property(e => e.LikedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("liked_at");

            entity.HasOne(d => d.Story).WithMany(p => p.UserLikes)
                .HasForeignKey(d => d.StoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Likes_Story");

            entity.HasOne(d => d.User).WithMany(p => p.UserLikes)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Likes_User");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.Role }).HasName("PK__User_Rol__31DDE51BA851ADFF");

            entity.ToTable("User_Roles");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("role");
            entity.Property(e => e.AssignedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("assigned_at");

            entity.HasOne(d => d.User).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_Roles");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
