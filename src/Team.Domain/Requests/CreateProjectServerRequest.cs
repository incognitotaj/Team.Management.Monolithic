namespace Team.Domain.Requests
{
    public class CreateProjectServerRequest
    {
        public Guid ProjectId { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
