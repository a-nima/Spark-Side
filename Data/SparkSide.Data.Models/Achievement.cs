namespace SparkSide.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using SparkSide.Data.Common.Models;

    public class Achievement : BaseDeletableModel<int>
    {
        public Achievement()
        {
            this.Users = new HashSet<ApplicationUser>();
        }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public string IconUrl { get; set; }

        public virtual ICollection<ApplicationUser> Users { get; set; }
    }
}
