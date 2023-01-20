namespace Team.Domain.Requests
{
    public class CreateProjectResourceRequest
    {
        public Guid ProjectId { get; set; }
        public Guid ResourceId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
