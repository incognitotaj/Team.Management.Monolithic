using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Team.Domain.Entities.Identity
{
    public class AppUser : IdentityUser
    {
        public AppUser()
        {
            Projects= new HashSet<Project>();
        }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public virtual ICollection<Project> Projects { get; set; }

    }
}
