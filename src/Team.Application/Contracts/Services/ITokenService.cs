using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team.Domain.Entities.Identity;

namespace Team.Application.Contracts.Services
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
