using System;

namespace Team.Application.Dtos
{
    public class ProjectResourceDailyTaskDto
    {
        public Guid Id { get; set; }
        public Guid ProjectResourceId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int TaskStatus { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
