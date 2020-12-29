namespace SparkSide.Web.ViewModels.Users
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using Microsoft.AspNetCore.Http;
    using SparkSide.Data.Models;

    public class UserSettingsInputModel
    {
        public UserSettingsInputModel()
        { }

        public UserSettingsInputModel(ApplicationUser user)
        {
            this.UserId = user.Id;
            this.FirstName = user.FirstName;
            this.LastName = user.LastName;
            this.Email = user.Email;
            this.Description = user.Description;
        }

        public string UserId { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(25)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(25)]
        public string LastName { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        public IFormFile ProfilePicture { get; set; }
    }
}
