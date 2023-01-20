namespace Team.Domain.Requests
{
    public class UpdateProjectClientRequest
    {
        public Guid ProjectClientId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
