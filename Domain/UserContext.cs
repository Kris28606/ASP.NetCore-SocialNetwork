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
        //public DbSet<Reaction> Reactions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb; Database=SocialNetwork; Trusted_Connection=True;");
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User>().OwnsMany(s => s.Posts).WithOwner(p => p.User);
            builder.Entity<User>().HasMany(u => u.Followers).WithMany(u => u.Following);
            //builder.Ignore<Post>();
            //builder.Entity<Reaction>().HasKey(r => new { r.PostId, r.UserId });
            //builder.Entity<Reaction>().HasOne(r => r.User).WithMany(u => u.Reactions);
            //builder.Entity<Reaction>().HasOne(r => r.Post).WithMany(p => p.Reactions);
            //builder.Entity<Reaction>().ToTable("Reactions");
            builder.Entity<Message>().HasKey(m => m.MessageId);
            builder.Entity<Message>().HasOne(m => m.FromUser).WithMany(s => s.Received).HasForeignKey(m => m.FromId);
            builder.Entity<Message>().HasOne(m => m.ForUser).WithMany(s => s.Send).HasForeignKey(m => m.ForId);
        }
    }
}
