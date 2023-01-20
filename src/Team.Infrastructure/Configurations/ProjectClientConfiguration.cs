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
    public class ProjectClientConfiguration : IEntityTypeConfiguration<ProjectClient>
    {
        public void Configure(EntityTypeBuilder<ProjectClient> builder)
        {
            builder.ToTable(name: "ProjectClients");

            builder.HasKey(x => x.Id)
                .HasName("PK_ProjectClients");

            builder.Property(x => x.Id)
                .HasDefaultValueSql("NEWID()");

            builder.Property(x => x.ProjectId)
                .IsRequired();

            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnType("NVARCHAR(100)");

            builder.Property(x => x.Email)
                .IsRequired()
                .HasColumnType("NVARCHAR(100)");

            builder.Property(x => x.Phone)
                .IsRequired()
                .HasColumnType("NVARCHAR(30)");

            builder.Property(x => x.CreatedOn)
               .IsRequired()
               .HasColumnType("DATETIME")
               .HasDefaultValueSql("GETDATE()");

            builder.HasOne(x => x.Project)
                .WithMany(x => x.ProjectClients)
                .HasForeignKey(x => x.ProjectId)
                .HasConstraintName("FK_ProjectClients_Projects")
                .IsRequired();
        }
    }
}
