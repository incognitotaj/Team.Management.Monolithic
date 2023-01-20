namespace Team.Application.Dtos
{
    public class ProjectResourceDto
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public Guid ResourceId { get; set; }
        public string Resource { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
