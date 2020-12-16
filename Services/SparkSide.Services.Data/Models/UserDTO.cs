namespace SparkSide.Services.Data.Models
{
    using SparkSide.Data.Models;

    public class UserDTO
    {
        public UserDTO(ApplicationUser user)
        {
            this.Id = user.Id;
            this.LoginName = user.LoginName;
            this.Email = user.Email;
            this.FirstName = user.FirstName;
            this.LastName = user.LastName;
            this.ProfileDescription = user.Description ?? string.Empty;
            this.ProfilePictureLink = user.ProfilePictureLink ?? string.Empty;
            this.FollowersCount = user.FollowedByUsers.Count;
            this.FollowingCount = user.FollowedUsers.Count;
        }

        public string Id { get; set; }

        public string LoginName { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int FollowersCount { get; set; }

        public int FollowingCount { get; set; }

        public string ProfilePictureLink { get; set; }

        public string ProfileDescription { get; set; }
    }
}
