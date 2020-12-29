using System;
using System.Collections.Generic;
using System.Text;

namespace SparkSide.Services.DTOs
{
    public class ActiveChallengeDTO
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int Progress { get; set; }

        public DateTime? StartedOn { get; set; }
    }
}
