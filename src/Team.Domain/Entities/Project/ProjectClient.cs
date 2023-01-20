using Team.Domain.Common;

namespace Team.Domain.Entities
{
    public class ProjectClient : EntityBase
    {
        public ProjectClient(Guid projectId, string name, string email, string phone)
        {
            ProjectId = projectId;
            Name = name;
            Email = email;
            Phone = phone;
        }

        public Guid ProjectId { get; set; }
        public virtual Project Project { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

    }
}
