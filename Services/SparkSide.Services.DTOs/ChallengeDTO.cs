namespace SparkSide.Services.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using SparkSide.Data.Models;
    using SparkSide.Services.DTOs;

    public class ChallengeDTO
    {
        public ChallengeDTO()
        { }

        public ChallengeDTO(Challenge challenge)
        {
            this.Id = challenge.Id;
            this.Title = challenge.Title;
            this.Description = challenge.Description;
            this.IsPublished = challenge.IsPublished;
            this.DurationDays = challenge.DurationDays;
            this.Author = new UserDTO(challenge.Author);
            this.CreatedOn = challenge.CreatedOn;
            this.PictureLink = challenge.PictureLink;
            this.Tags = challenge.Tags.Select(t => t.Tag.Title).ToList();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int DurationDays { get; set; }

        public string PictureLink { get; set; }

        public DateTime CreatedOn { get; set; }

        public UserDTO Author { get; set; }

        public bool IsPublished { get; set; }

        public IEnumerable<ChallengeTaskDTO> Tasks { get; set; }

        public ICollection<string> Tags { get; set; }
    }
}
