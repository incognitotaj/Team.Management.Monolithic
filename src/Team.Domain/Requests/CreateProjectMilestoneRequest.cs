namespace Team.Domain.Requests
{
    public class CreateProjectMilestoneRequest
    {
        public string Title { get; set; }
        public string Detail { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
