namespace Team.Domain.Requests
{
    public class UpdateProjectMilestoneRequest
    {
        public Guid ProjectMilestoneId { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
