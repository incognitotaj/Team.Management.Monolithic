using Team.Domain.Common;

namespace Team.Domain.Entities
{
    public class ProjectServer : EntityBase
    {
        public ProjectServer(Guid projectId, string title, string url, string username, string password)
        {
            ProjectId = projectId;
            Title = title;
            Url = url;
            Username = username;
            Password = password;
        }

        public Guid ProjectId { get; set; }
        public virtual Project Project { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

    }
}
