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

    public class SiteAlertConfiguration : IEntityTypeConfiguration<SiteAlert>
    {
        public void Configure(EntityTypeBuilder<SiteAlert> alert)
        {
            alert
                .Property(a => a.AlertType)
                .HasConversion<int>()
                .IsRequired();
        }
    }
}
