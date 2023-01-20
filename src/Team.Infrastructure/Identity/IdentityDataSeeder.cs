using Microsoft.AspNetCore.Identity;
using Team.Domain.Entities.Identity;

namespace Team.Infrastructure.Identity
{
    public class IdentityDataSeeder
    {
        public static async Task SeedAsync(UserManager<AppUser> _userManager)
        {
            if (!_userManager.Users.Any())
            {
                var user = new AppUser
                {
                    Name = "Salman Taj",
                    Email = "salman1277@gmail.com",
                    UserName = "salman1277",
                    PhoneNumber = "+923308451234",
                };

                await _userManager.CreateAsync(user, "Pa$$w0rd");
            }
        }
    }
}
