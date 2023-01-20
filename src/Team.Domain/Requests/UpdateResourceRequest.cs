namespace Team.Domain.Requests
{
    public class UpdateResourceRequest
    {
        public Guid ResourceId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
