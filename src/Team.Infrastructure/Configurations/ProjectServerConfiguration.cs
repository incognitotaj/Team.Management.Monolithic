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
    public class ProjectServerConfiguration : IEntityTypeConfiguration<ProjectServer>
    {
        public void Configure(EntityTypeBuilder<ProjectServer> builder)
        {
            builder.ToTable(name: "ProjectServers");

            builder.HasKey(x => x.Id)
                .HasName("PK_ProjectServers");

            builder.Property(x => x.Id)
                .HasDefaultValueSql("NEWID()");

            builder.Property(x => x.ProjectId)
                .IsRequired();

            builder.Property(x => x.Title)
                .IsRequired()
                .HasColumnType("NVARCHAR(150)");

            builder.Property(x => x.Url)
                .IsRequired()
                .HasColumnType("NVARCHAR(256)");

            builder.Property(x => x.Username)
                .IsRequired()
                .HasColumnType("NVARCHAR(50)");

            builder.Property(x => x.Password)
                .IsRequired()
                .HasColumnType("NVARCHAR(50)");

            builder.Property(x => x.CreatedOn)
               .IsRequired()
               .HasColumnType("DATETIME")
               .HasDefaultValueSql("GETDATE()");

            builder.HasOne(x => x.Project)
                .WithMany(x => x.ProjectServers)
                .HasForeignKey(x => x.ProjectId)
                .HasConstraintName("FK_ProjectServers_Projects")
                .IsRequired();
        }
    }
}
