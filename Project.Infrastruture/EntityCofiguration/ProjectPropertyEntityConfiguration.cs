using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Infrastruture.EntityCofiguration
{
    public class ProjectPropertyEntityConfiguration : IEntityTypeConfiguration<Domain.AggregatesModel.ProjectProperty>
    {
        public void Configure(EntityTypeBuilder<Domain.AggregatesModel.ProjectProperty> builder)
        {
            builder
           .ToTable("ProjectPropertys")
           .Property(p => p.Key).HasMaxLength(100);
            builder
           .Property(p => p.Value).HasMaxLength(100);
            builder.HasKey(p => new { p.ProjectId,p.Key,p.Value});
        }
    }
}
