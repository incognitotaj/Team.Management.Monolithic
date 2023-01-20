namespace Team.Application.Dtos
{
    public class ProjectDocumentDto
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
        public string Title { get; set; }
        public string FilePath { get; set; }
        public string Detail { get; set; }
    }
}
