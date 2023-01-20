using Team.Domain.Common;

namespace Team.Domain.Entities
{
    public class ProjectMilestone : EntityBase
    {
        public ProjectMilestone(Guid projectId, string title, string detail, DateTime fromDate, DateTime? toDate)
        {
            ProjectId = projectId;
            Title = title;
            Detail = detail;
            FromDate = fromDate;
            ToDate = toDate;
        }

        public Guid ProjectId { get; set; }
        public virtual Project Project { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime? ToDate { get; set; }

    }
}
