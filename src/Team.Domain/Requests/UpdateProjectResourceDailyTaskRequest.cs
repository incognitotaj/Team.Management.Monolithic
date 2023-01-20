namespace Team.Domain.Requests
{
    public class UpdateProjectResourceDailyTaskRequest
    {
        public Guid ProjectResourceDailyTaskId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int TaskStatus { get; set; }
    }
}
