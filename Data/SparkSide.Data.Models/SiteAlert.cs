namespace SparkSide.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using SparkSide.Data.Common.Enums;
    using SparkSide.Data.Common.Models;

    public class SiteAlert : BaseDeletableModel<int>
    {
        [Required]
        [MaxLength(250)]
        public string Title { get; set; }

        [Required]
        public AlertType AlertType { get; set; }

        [Required]
        public string AuthorId { get; set; }

        public virtual ApplicationUser Author { get; set; }
    }
}
