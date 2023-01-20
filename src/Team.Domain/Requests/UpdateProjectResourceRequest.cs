namespace Team.Domain.Requests
{
    public class UpdateProjectResourceRequest
    {
        public Guid ProjectResourceId { get; set; }
        public Guid ProjectId { get; set; }
        public Guid ResourceId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
