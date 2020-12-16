namespace SparkSide.Data.Configurations
{
    using SparkSide.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> appUser)
        {
            appUser
                .HasMany(e => e.Claims)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            appUser
                .HasMany(e => e.Logins)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            appUser
                .HasMany(e => e.Roles)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            appUser
                .HasMany(e => e.ActiveChallenges)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            appUser
               .HasMany(e => e.Comments)
               .WithOne()
               .HasForeignKey(e => e.AuthorId)
               .OnDelete(DeleteBehavior.Restrict);

            appUser
               .HasMany(e => e.CreatedChallenges)
               .WithOne(e => e.Author)
               .HasForeignKey(e => e.AuthorId)
               .OnDelete(DeleteBehavior.Restrict);

            appUser
               .HasMany(e => e.FavouriteChallenges)
               .WithOne()
               .HasForeignKey(e => e.UserId)
               .OnDelete(DeleteBehavior.Restrict);

            appUser
               .HasMany(e => e.SiteAlerts)
               .WithOne()
               .HasForeignKey(e => e.AuthorId)
               .OnDelete(DeleteBehavior.Restrict);

            appUser
                .HasMany(e => e.FollowedUsers)
                .WithOne(e => e.Follower)
                .HasForeignKey(e => e.FollowerId)
                .OnDelete(DeleteBehavior.Restrict);
            
            appUser
                .HasMany(e => e.FollowedByUsers)
                .WithOne(e => e.Followed)
                .HasForeignKey(e => e.FollowedId)
                .OnDelete(DeleteBehavior.Restrict);

            appUser
                .HasIndex(e => e.Username)
                    .IsUnique();
        }
    }
}
