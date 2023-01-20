using Team.Domain.Common;

namespace Team.Domain.Entities
{
    public class ProjectResourceDailyTask : EntityBase
    {
        public ProjectResourceDailyTask(Guid projectResourceId, string title, string description, Enums.TaskStatus taskStatus)
        {
            ProjectResourceId = projectResourceId;
            Title = title;
            Description = description;
            TaskStatus = taskStatus;
        }

        public Guid ProjectResourceId { get; set; }
        public virtual ProjectResource ProjectResource { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Enums.TaskStatus TaskStatus { get; set; }
    }
}
