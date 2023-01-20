using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Team.Domain.Entities;

namespace Team.Infrastructure.Configurations
{
    public class ProjectResourceDailyTaskConfiguration : IEntityTypeConfiguration<ProjectResourceDailyTask>
    {
        public void Configure(EntityTypeBuilder<ProjectResourceDailyTask> builder)
        {
            builder.ToTable(name: "ProjectResourceDailyTasks");

            builder.HasKey(x => x.Id)
                .HasName("PK_DailyTasks");

            builder.Property(x => x.Id)
                .HasDefaultValueSql("NEWID()");

            builder.Property(x => x.ProjectResourceId)
                .IsRequired();

            builder.Property(x => x.TaskStatus)
                .HasColumnType("int")
                .IsRequired();

            builder.Property(x => x.Title)
                .IsRequired()
                .HasColumnType("NVARCHAR(150)");

            builder.Property(x => x.Description)
                .IsRequired(false)
                .HasColumnType("NVARCHAR(500)");

            builder.Property(x => x.CreatedOn)
               .IsRequired()
               .HasColumnType("DATETIME")
               .HasDefaultValueSql("GETDATE()");

            builder.HasOne(x => x.ProjectResource)
                .WithMany(x => x.DailyTasks)
                .HasForeignKey(x => x.ProjectResourceId)
                .HasConstraintName("FK_DailyTasks_ProjectResources")
                .IsRequired();
        }
    }
}
