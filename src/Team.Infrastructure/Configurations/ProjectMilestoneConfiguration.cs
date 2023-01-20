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
    public class ProjectMilestoneConfiguration : IEntityTypeConfiguration<ProjectMilestone>
    {
        public void Configure(EntityTypeBuilder<ProjectMilestone> builder)
        {
            builder.ToTable(name: "ProjectMilestones");

            builder.HasKey(x => x.Id)
                .HasName("PK_ProjectMilestones");

            builder.Property(x => x.Id)
                .HasDefaultValueSql("NEWID()");

            builder.Property(x => x.ProjectId)
                .IsRequired();

            builder.Property(x => x.Title)
               .IsRequired()
               .HasColumnType("NVARCHAR(100)");

            builder.Property(x => x.Detail)
               .IsRequired()
               .HasColumnType("NVARCHAR(500)");

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
                .WithMany(x => x.ProjectMilestones)
                .HasForeignKey(x => x.ProjectId)
                .HasConstraintName("FK_ProjectMilestones_Projects")
                .IsRequired();

        }
    }
}
