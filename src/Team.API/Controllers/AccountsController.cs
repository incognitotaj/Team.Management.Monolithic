using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Team.Application.Contracts.Services;
using Team.Application.Dtos;
using Team.Domain.Entities.Identity;
using Team.Domain.Requests;
using Team.Infrastructure.Extensions;

namespace Team.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AccountsController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        [HttpGet("user-info")]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var user = await _userManager.FindByEmailFromClaimsPrincipalAsync(User);

            return Ok(new UserDto
            {
                Email = user.Email,
                Name = user.Name,
                Token = _tokenService.CreateToken(user),
                Username = user.UserName
            });
        }

        [HttpGet("user-projects")]
        [Authorize]
        public async Task<ActionResult> GetUserProjects()
        {
            var userProjects = await _userManager.FindProjectsByEmailFromClaimsPrincipalAsync(User);
            if (userProjects == null)
            {
                return Unauthorized();
            }
            var projects = _mapper.Map<IEnumerable<ProjectDto>>(userProjects.Projects);

            return Ok(projects);
        }


        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.Username);

            if (user == null)
            {
                return Unauthorized();
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!result.Succeeded)
            {
                return Unauthorized();
            }


            return Ok(new UserDto
            {
                Email = user.Email,
                Name = user.Name,
                Token = _tokenService.CreateToken(user),
                Username = user.UserName
            });
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterUserRequest request)
        {
            var user = new AppUser
            {
                Email = request.Email,
                Name = request.Name,
                UserName = request.Username,
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                return BadRequest();
            }

            return Ok(new UserDto
            {
                Email = user.Email,
                Name = user.Name,
                Token = _tokenService.CreateToken(user),
                Username = user.UserName
            });
        }
    }
}
