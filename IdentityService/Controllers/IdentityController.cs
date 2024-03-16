using Common.Constants;
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

        [Authorize(Roles = UserConstants.RoleAdmin, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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

        [HttpPost("getallgroups")]
        public async Task<PaginatedResponse<GetAllGroupsResponseDto>> GetAllGroups(PaginationRequest paginationRequest)
        {
            return await _userService.GetAllGroups(paginationRequest);
        }

        [HttpGet("getgroupinfobyid")]
        public async Task<GetGroupInfoResponseDto> GetGroupInfoById(Guid immutableId)
        {
            return await _userService.GetGroupInfoById(immutableId);
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
        public async Task<Guid> CreateNewGroup(CreateNewGroupRequest dto)
        {
            return await _userService.CreateNewGroupAsync(dto);
        }

        [HttpGet("getuserbyid")]
        public async Task<FindUserResponseDto> GetUserById([FromQuery]Guid userId)
        {
            return await _userService.GetUserById(userId);
        }

        [HttpPost("updategroup")]
        public async Task UpdateGroup(UpdateGroupRequestDto dto)
        {
            await _userService.UpdateGroup(dto);
        }

        [HttpGet("getgroupsforuser")]
        public async Task<List<Guid>> GetGroupsForUser(Guid userId)
        {
            return await _userService.GetGroupsForUser(userId);
        }
    }
}
