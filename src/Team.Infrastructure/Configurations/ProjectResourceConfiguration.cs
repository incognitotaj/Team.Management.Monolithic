using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team.Domain.Entities;

namespace Team.Infrastructure.Configurations
{
    public class ProjectResourceConfiguration : IEntityTypeConfiguration<ProjectResource>
    {
        public void Configure(EntityTypeBuilder<ProjectResource> builder)
        {
            builder.ToTable(name: "ProjectResources");

            builder.HasKey(x => x.Id)
                .HasName("PK_ProjectResources");

            builder.Property(x => x.Id)
                .HasDefaultValueSql("NEWID()");

            builder.Property(x => x.ProjectId)
                .IsRequired();

            builder.Property(x => x.ResourceId)
                .IsRequired();

            builder.Property(x => x.FromDate)
                .IsRequired()
                .HasColumnType("DATETIME");

            builder.Property(x => x.ToDate)
                .IsRequired(false)
                .HasColumnType("DATETIME");

            builder.Property(x => x.CreatedOn)
               .IsRequired()
               .HasColumnType("DATETIME")
               .HasDefaultValueSql("GETDATE()");

            builder.HasOne(x => x.Project)
                .WithMany(x => x.ProjectResources)
                .HasForeignKey(x => x.ProjectId)
                .HasConstraintName("FK_ProjectResources_Projects")
                .IsRequired();

            builder.HasOne(x => x.Resource)
                .WithMany(x => x.ProjectResources)
                .HasForeignKey(x => x.ResourceId)
                .HasConstraintName("FK_ProjectResources_Resources")
                .IsRequired();
        }
    }
}
