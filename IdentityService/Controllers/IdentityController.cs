using IdentityService.Data.Contracts.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Common.Exceptions;

namespace IdentityService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IdentityController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _config;

        public IdentityController(UserManager<IdentityUser> userManager,
                                  SignInManager<IdentityUser> signInManager,
                                  IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
        }

        [HttpGet("hello")]
        public string Hello() => "hello";

        [Authorize(Roles = "Admin")]
        [HttpPost("create")]
        public async Task CreatuUser([FromBody] CreateUserRequestDto request)
        {
            IdentityResult res = await _userManager.CreateAsync(new IdentityUser
            {
                UserName = request.UserName,
                Email = request.Email,
            },
            request.Password);

            if (res.Succeeded)
            {
                Console.WriteLine("created");
            }
            else
            {
                StringBuilder stringBuilder = new StringBuilder();
                foreach (var err in res.Errors)
                {
                    stringBuilder.Append(err.Description);
                    stringBuilder.Append("\n");
                }
                Console.WriteLine($"{stringBuilder.ToString()}");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("find")]
        public async Task<object> FindUser(string email)
        {
            IdentityUser? res = await _userManager.FindByEmailAsync(email);
            if (res == null)
            {
                return "nothing";
            }

            return new { Email = res.Email, Name = res.UserName };
        }

        [AllowAnonymous]
        [HttpPost("signin")]
        public async Task<SignInResponseDto> SignIn([FromBody] SignInDto dto)
        {
            Microsoft.AspNetCore.Identity.SignInResult res = await _signInManager.PasswordSignInAsync(dto.UserName, dto.Password, false, false);
            if (!res.Succeeded)
                throw new AuthException("No match");

            IdentityUser user = await _userManager.FindByNameAsync(dto.UserName) ?? throw new Exception();
            IList<string> roles = await _userManager.GetRolesAsync(user);
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            Claim[] claims = roles.Select(role => new Claim(ClaimTypes.Role, role)).ToArray();

            JwtSecurityToken token = new JwtSecurityToken(_config["Jwt:Issuer"],
                                             _config["Jwt:Audience"],
                                             claims,
                                             null,
                                             DateTime.Now.AddDays(1),
                                             credentials);

            return new SignInResponseDto
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                UserName = user.UserName,
                Role = roles.First()
            };
        }
    }
}
