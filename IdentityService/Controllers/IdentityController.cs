using IdentityService.Data.Contracts.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace IdentityService.Controllers
{
    public class IdentityController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        public IdentityController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet("hello")]
        public string Hello() => "hello";

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
    }
}
