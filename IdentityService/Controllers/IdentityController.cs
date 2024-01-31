using Common.Dto;
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
        public async Task<PaginatedResponse<GetAllGroupsResponseDto>> GetAllGroups(PaginationRequest paginationRequest)
        {
            return await _userService.GetAllGroups(paginationRequest);
        }

        [HttpPost("find")]
        public async Task<PaginatedResponse<FindUserResponseDto>> Find([FromBody]PaginationRequest<FindRequestDto> paginationRequest)
        {
            return await _userService.FindUser(paginationRequest);
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
