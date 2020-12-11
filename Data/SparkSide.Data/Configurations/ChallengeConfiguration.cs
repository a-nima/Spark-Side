namespace SparkSide.Data.Configurations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using SparkSide.Data.Models;

    public class ChallengeConfiguration : IEntityTypeConfiguration<Challenge>
    {
        public void Configure(EntityTypeBuilder<Challenge> challenge)
        {
            challenge
               .HasMany(e => e.Comments)
               .WithOne()
               .HasForeignKey(e => e.ChallengeId)
               .OnDelete(DeleteBehavior.Restrict);

            challenge
               .HasMany(e => e.UsersWithActiveChallenge)
               .WithOne()
               .HasForeignKey(e => e.ChallengeId)
               .OnDelete(DeleteBehavior.Restrict);

            challenge
               .HasMany(e => e.UsersWithActiveChallenge)
               .WithOne()
               .HasForeignKey(e => e.ChallengeId)
               .OnDelete(DeleteBehavior.Restrict);

            challenge
               .HasMany(e => e.Tags)
               .WithOne()
               .HasForeignKey(e => e.ChallengeId)
               .OnDelete(DeleteBehavior.Restrict);

            challenge
                .HasMany(e => e.ChallengeTasks)
                .WithOne()
                .HasForeignKey(e => e.ChallengeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
