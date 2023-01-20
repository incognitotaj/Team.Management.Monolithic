namespace Team.Domain.Requests
{
    public class UpdateProjectServerRequest
    {
        public Guid ProjectServerId { get; set; }
        public Guid ProjectId { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
