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
    public class ResourceConfiguration : IEntityTypeConfiguration<Resource>
    {
        public void Configure(EntityTypeBuilder<Resource> builder)
        {
            builder.ToTable(name: "Resources");

            builder.HasKey(x => x.Id)
                .HasName("PK_Resources");

            builder.Property(x => x.Id)
                .HasDefaultValueSql("NEWID()");

            builder.Property(x => x.LastName)
                .IsRequired()
                .HasColumnType("NVARCHAR(25)");

            builder.Property(x => x.LastName)
                .IsRequired()
                .HasColumnType("NVARCHAR(25)");

            builder.Property(x => x.Email)
                .IsRequired(false)
                .HasColumnType("NVARCHAR(100)");

            builder.Property(x => x.Phone)
                .IsRequired(false)
                .HasColumnType("NVARCHAR(30)");

            builder.Property(x => x.CreatedOn)
               .IsRequired()
               .HasColumnType("DATETIME")
               .HasDefaultValueSql("GETDATE()");
        }
    }
}
