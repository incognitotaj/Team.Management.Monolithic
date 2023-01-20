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
    public class ProjectDocumentConfiguration : IEntityTypeConfiguration<ProjectDocument>
    {
        public void Configure(EntityTypeBuilder<ProjectDocument> builder)
        {
            builder.ToTable(name: "ProjectDocuments");

            builder.HasKey(x => x.Id)
                .HasName("PK_ProjectDocuments");

            builder.Property(x => x.Id)
                .HasDefaultValueSql("NEWID()");

            builder.Property(x => x.ProjectId)
                .IsRequired();

            builder.Property(x => x.Title)
               .IsRequired()
               .HasColumnType("NVARCHAR(100)");

            builder.Property(x => x.FilePath)
               .IsRequired()
               .HasColumnType("NVARCHAR(256)");

            builder.Property(x => x.Detail)
               .IsRequired()
               .HasColumnType("NVARCHAR(500)");

            builder.Property(x => x.CreatedOn)
               .IsRequired()
               .HasColumnType("DATETIME")
               .HasDefaultValueSql("GETDATE()");

            builder.HasOne(x => x.Project)
                .WithMany(x => x.ProjectDocuments)
                .HasForeignKey(x => x.ProjectId)
                .HasConstraintName("FK_ProjectDocuments_Projects")
                .IsRequired();

        }
    }
}
