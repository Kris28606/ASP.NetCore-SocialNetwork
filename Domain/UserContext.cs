using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class UserContext : IdentityDbContext<User, IdentityRole<int>, int>
    {

        public UserContext([NotNull] DbContextOptions options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Reaction> Reactions { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public DbSet<Notification> Notifications { get; set; }
        public DbSet<LikeNotification> LikeNotifications { get; set; }
        public DbSet<CommentNotification> CommentNotification { get; set; }
        public DbSet<FollowNotification> FollowNotifications { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb; Database=SocialNetwork; Trusted_Connection=True;");
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User>().HasMany(s => s.Posts).WithOne(p => p.User);

            builder.Entity<User>().HasMany(u => u.Followers).WithMany(u => u.Following);

            builder.Entity<Reaction>().HasKey(r => new { r.PostId, r.UserId });
            builder.Entity<Reaction>().HasOne(r => r.User).WithMany(u => u.Reactions);
            builder.Entity<Reaction>().HasOne(r => r.Post).WithMany(p => p.Reactions);
            builder.Entity<Reaction>().ToTable("Reactions");

            builder.Entity<Message>().HasKey(m => m.MessageId);
            builder.Entity<Message>().HasOne(m => m.FromUser).WithMany(s => s.Received).HasForeignKey(m => m.FromId);
            builder.Entity<Message>().HasOne(m => m.ForUser).WithMany(s => s.Send).HasForeignKey(m => m.ForId);

            builder.Entity<Comment>().HasKey(c => new { c.UserId, c.PostId, c.DatumVreme });
            builder.Entity<Comment>().HasOne(c => c.Post).WithMany(p => p.Comments);
            builder.Entity<Comment>().HasOne(c => c.User).WithMany(u => u.Comments);
            builder.Entity<Comment>().ToTable("Comments");

            builder.Entity<Notification>().HasKey(n => new { n.ForWhoId, n.FromWhoId, n.Date });

            builder.Entity<LikeNotification>().HasOne(c => c.Post).WithMany(p => p.LikeNotifications);
            builder.Entity<CommentNotification>().HasOne(c => c.Post).WithMany(p => p.CommentNotifications);
            builder.Entity<Notification>().HasOne(n => n.FromWho).WithMany(u => u.NotificationsFromMe);
            builder.Entity<Notification>().HasOne(n => n.ForWho).WithMany(u => u.MyNotifications);

            builder.Entity<LikeNotification>().HasBaseType<Notification>().ToTable("LikeNotifications");
            builder.Entity<CommentNotification>().HasBaseType<Notification>().ToTable("CommentNotifications");
            builder.Entity<FollowNotification>().HasBaseType<Notification>().ToTable("FollowNotifications");
        
        }
    }
}
