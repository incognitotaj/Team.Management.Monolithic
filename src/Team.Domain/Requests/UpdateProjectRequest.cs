namespace Team.Domain.Requests
{
    public class UpdateProjectRequest
    {
        public Guid ProjectId { get; set; }
        public string Name { get; set; }
        public string Detail { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
