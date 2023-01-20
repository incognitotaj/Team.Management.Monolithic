using Team.Domain.Common;

namespace Team.Domain.Entities
{
    public class Resource : EntityBase
    {
        public Resource(string firstName, string lastName, string email, string phone)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Phone = phone;

            ProjectResources = new HashSet<ProjectResource>();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public virtual ICollection<ProjectResource> ProjectResources { get; set; }

    }
}
