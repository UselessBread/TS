using Common.Constants;
using Common.Dto;
using Common.Exceptions;
using IdentityService.Data.Contracts.DTO;
using IdentityService.Data.Contracts.Entities;
using IdentityService.Data.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IdentityService.Services
{
    public interface IUserService
    {
        public Task CreateUser( CreateUserRequestDto request);
        public Task<SignInResponseDto> SignIn(SignInDto dto);
        public Task<PaginatedResponse<GetAllGroupsResponseDto>> GetAllGroups(PaginationRequest paginationRequest);
        public Task<GetGroupInfoResponseDto> GetGroupInfoById(Guid immutableId);
        public Task<PaginatedResponse<FindUserResponseDto>> FindUser(PaginationRequest<FindRequestDto> pagination);
        public Task AddStudentsToGroup(AddStudentsToGroupRequest dto);
        public Task<Guid> CreateNewGroupAsync(CreateNewGroupRequest dto);
        public Task<FindUserResponseDto> GetUserById(Guid userId);
        public Task UpdateGroup(UpdateGroupRequestDto dto);
        public Task<List<Guid>> GetGroupsForUser(Guid userId);
    }

    public class UserService : IUserService
    {
        private readonly UserManager<TsUser> _userManager;
        private readonly SignInManager<TsUser> _signInManager;
        private readonly IUserRolesRepository _identRep;
        private readonly IGroupsRepository _groupsRepository;
        private readonly IStudentsByGroupsRepository _studentsByGroupsRepository;
        private readonly IRolesRepository _rolesRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly IConfiguration _config;

        public UserService(UserManager<TsUser> userManager,
                           SignInManager<TsUser> signInManager,
                           IUserRolesRepository identRep,
                           IGroupsRepository groupsRepository,
                           IStudentsByGroupsRepository studentsByGroupsRepository,
                           IRolesRepository rolesRepository,
                           IUsersRepository usersRepository,
                           IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _identRep = identRep;
            _groupsRepository = groupsRepository;
            _studentsByGroupsRepository = studentsByGroupsRepository;
            _rolesRepository = rolesRepository;
            _usersRepository = usersRepository;
            _config = config;
        }

        public async Task<FindUserResponseDto> GetUserById(Guid userId)
        {
            TsUser res = await _userManager.FindByIdAsync(userId.ToString()) ??
                throw new BadRequestException($"No user with id = {userId} was found");

            return new FindUserResponseDto
            {
                Email = res.Email ?? string.Empty,
                Name = res.Name,
                Surname = res.Surname,
                UserId = res.Id
            };
        }

        public async Task CreateUser(CreateUserRequestDto request)
        {
            IdentityResult res = await _userManager.CreateAsync(new TsUser
            {
                UserName = request.UserName,
                Email = request.Email,
                Name = request.Name,
                Surname = request.Surname
            },
            request.Password);
            CheckIdentityResult(res);

            TsUser? createdUser = await _userManager.FindByNameAsync(request.UserName);
            if (createdUser == null)
            {
                throw new BadRequestException("User was not created");
            }

            switch (request.UserType)
            {
                case UserTypes.Admin:
                    res = await _userManager.AddToRoleAsync(createdUser, UserConstants.RoleAdmin);
                    break;
                case UserTypes.Teacher:
                    res = await _userManager.AddToRoleAsync(createdUser, UserConstants.RoleTeacher);
                    break;
                case UserTypes.Student:
                    res = await _userManager.AddToRoleAsync(createdUser, UserConstants.RoleStudent);
                    break;
                default:
                    throw new BadRequestException("This role was not specified");
            }

            CheckIdentityResult(res);
        }

        public async Task<SignInResponseDto> SignIn(SignInDto dto)
        {
            SignInResult res = await _signInManager.PasswordSignInAsync(dto.UserName, dto.Password, false, false);
            if (!res.Succeeded)
                throw new AuthException("No match");

            TsUser user = await _userManager.FindByNameAsync(dto.UserName)
                ?? throw new EntityNotFoundException("No such user was found");

            IList<string> roles = await _userManager.GetRolesAsync(user);
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            List<Claim> claims = roles.Select(role => new Claim(ClaimTypes.Role, role)).ToList();
            claims.Add(new Claim(JwtClaimsConstants.CLaimUserId, user.Id.ToString()));

            JwtSecurityToken token = new JwtSecurityToken(_config["Jwt:Issuer"],
                                             _config["Jwt:Audience"],
                                             claims,
                                             null,
                                             DateTime.Now.AddDays(1),
                                             credentials);

            return new SignInResponseDto
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                UserName = user.UserName ?? throw new InvalidContentException("UserName cannot be null"),
                Role = roles.First()
            };
        }

        public async Task<PaginatedResponse<GetAllGroupsResponseDto>> GetAllGroups(PaginationRequest paginationRequest)
        {
            return await _groupsRepository.GetAllGroupsAsync(paginationRequest);
        }

        public async Task<GetGroupInfoResponseDto> GetGroupInfoById(Guid immutableId)
        {
            Groups group = await _groupsRepository.GetByImmutableId(immutableId);
            List<Guid> studs = await _studentsByGroupsRepository.GetUsersInGroup(group.ImmutableId);

            return new GetGroupInfoResponseDto
            {
                GroupName = group.Name,
                StudentIds = studs,
                TeacherId = group.TeacherId
            };
        }

        public async Task<PaginatedResponse<FindUserResponseDto>> FindUser(PaginationRequest<FindRequestDto> paginationRequest)
        {
            IQueryable<IdentityRole<Guid>> roles = _rolesRepository.GetAllRolesAsQuery();
            IQueryable<TsUser> users = _usersRepository.GetAllUsersAsQuery();

            return await _identRep.FindUserAsync(paginationRequest, roles, users);
        }

        public async Task AddStudentsToGroup(AddStudentsToGroupRequest dto)
        {
            if (!await _groupsRepository.IsExists(dto.GroupImmutableId))
                throw new EntityNotFoundException("Such group does not exist");

            await _studentsByGroupsRepository.AddStudentsToGroup(dto);
        }

        public async Task<Guid> CreateNewGroupAsync(CreateNewGroupRequest dto)
        {
            IQueryable<IdentityRole<Guid>> roles = _rolesRepository.GetAllRolesAsQuery();
            if (!await _identRep.CheckIfHasRole(dto.TeacherId, UserConstants.RoleTeacher, roles))
                throw new BadRequestException("provided user was not teacher or does not exist");

            return await _groupsRepository.CreateNewGroup(dto);
        }

        public async Task UpdateGroup(UpdateGroupRequestDto dto)
        {
            await _groupsRepository.UpdateGroup(dto);
        }

        public async Task<List<Guid>> GetGroupsForUser(Guid userId)
        {
            return await _studentsByGroupsRepository.GetGroupsForUser(userId);
        }

        private static void CheckIdentityResult(IdentityResult res)
        {
            if (!res.Succeeded)
            {
                StringBuilder stringBuilder = new StringBuilder();
                foreach (IdentityError err in res.Errors)
                {
                    stringBuilder.Append(err.Description);
                    stringBuilder.Append("\n");
                }

                throw new BadRequestException(stringBuilder.ToString());
            }
        }
    }
}
