using SparkSide.Data.Models;

namespace SparkSide.Services.DTOs
{
    public class ChallengeTaskDTO
    {
        public ChallengeTaskDTO()
        { }

        public ChallengeTaskDTO(ChallengeTask input)
        {
            this.Id = input.Id;
            this.Day = input.Day;
            this.Title = input.Title;
            this.ChallengeId = input.ChallengeId;
        }

        public int Id { get; set; }

        public int Day { get; set; }

        public string Title { get; set; }

        public int ChallengeId { get; set; }
    }
}
