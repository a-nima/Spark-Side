// ReSharper disable VirtualMemberCallInConstructor
namespace SparkSide.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Identity;
    using SparkSide.Data.Common.Models;

    public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();
            this.Comments = new HashSet<Comment>();
            this.CreatedChallenges = new HashSet<Challenge>();
            this.ActiveChallenges = new HashSet<UserChallengeActive>();
            this.FavouriteChallenges = new HashSet<UserChallengeFavourite>();
            this.FollowedUsers = new HashSet<UserFollow>();
            this.FollowedByUsers = new HashSet<UserFollow>();
            this.SiteAlerts = new HashSet<SiteAlert>();
        }

        [Required]
        [MaxLength(50)]
        [RegularExpression(@"^[a-zA-Z.\-]+$", ErrorMessage = "Only  '.' and '-' special characters are allowed.")]
        public string LoginName { get; set; }

        public string ProfilePictureLink { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [MaxLength(25)]
        public string FirstName { get; set; }

        [MaxLength(25)]
        public string LastName { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }

        public virtual ICollection<Challenge> CreatedChallenges { get; set; }

        public virtual ICollection<UserChallengeActive> ActiveChallenges { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<UserChallengeFavourite> FavouriteChallenges { get; set; }

        public virtual ICollection<UserFollow> FollowedUsers { get; set; }

        public virtual ICollection<UserFollow> FollowedByUsers { get; set; }

        public virtual ICollection<SiteAlert> SiteAlerts { get; set; }
    }
}
