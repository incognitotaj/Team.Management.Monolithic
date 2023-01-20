namespace Team.Domain.Requests
{
    public class CreateProjectResourceDailyTaskRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int TaskStatus { get; set; }
    }
}
