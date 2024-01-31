using IdentityService.Data.Contracts.DTO;
using IdentityService.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IdentityController : Controller
    {
        private readonly IUserService _userService;

        public IdentityController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize(Roles = "Admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("create")]
        public async Task CreateUser([FromBody] CreateUserRequestDto request)
        {
            await _userService.CreateUser(request);
        }

        [AllowAnonymous]
        [HttpPost("signin")]
        public async Task<SignInResponseDto> SignIn([FromBody] SignInDto dto)
        {
            return await _userService.SignIn(dto);
        }

        //[Authorize(Roles = "Admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("getallgroups")]
        public async Task<List<GetAllGroupsResponseDto>> GetAllGroups()
        {
            return await _userService.GetAllGroups();
        }

        [HttpPost("find")]
        public async Task<List<FindUserResponseDto>> Find([FromBody]FindRequestDto dto)
        {
            return await _userService.FindUser(dto);
        }

        [HttpPost("addstudentstogroup")]
        public async Task AddStudentsToGroup([FromBody]AddStudentsToGroupRequest dto)
        {
            await _userService.AddStudentsToGroup(dto);
        }

        [HttpPost("creategroup")]
        public async Task CreateNewGroup(CreateNewGroupRequest dto)
        {
            await _userService.CreateNewGroupAsync(dto);
        }
    }
}
