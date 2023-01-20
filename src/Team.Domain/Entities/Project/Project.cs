using Team.Domain.Common;
using Team.Domain.Entities.Identity;

namespace Team.Domain.Entities
{
    public class Project : EntityBase
    {
        public Project(string name, string detail, DateTime startDate, DateTime endDate)
        {
            Name = name;
            Detail = detail;
            StartDate = startDate;
            EndDate = endDate;

            ProjectDocuments= new HashSet<ProjectDocument>();
            ProjectResources = new HashSet<ProjectResource>();
            ProjectClients = new HashSet<ProjectClient>();
            ProjectServers = new HashSet<ProjectServer>();
            ProjectMilestones = new HashSet<ProjectMilestone>();
        }

        public string Name { get; set; }
        public string Detail { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string ManagerId { get; set; }
        public virtual AppUser Manager { get; set; }

        public virtual ICollection<ProjectDocument> ProjectDocuments { get; set; }
        public virtual ICollection<ProjectResource> ProjectResources { get; set; }
        public virtual ICollection<ProjectClient> ProjectClients { get; set; }
        public virtual ICollection<ProjectServer> ProjectServers { get; set; }
        public virtual ICollection<ProjectMilestone> ProjectMilestones { get; set; }
    }
}
