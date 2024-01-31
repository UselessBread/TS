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
        public Task<List<GetAllGroupsResponseDto>> GetAllGroups();
        public Task<List<FindUserResponseDto>> FindUser(PaginationRequest<FindRequestDto> pagination);
        public Task AddStudentsToGroup(AddStudentsToGroupRequest dto);
        public Task CreateNewGroupAsync(CreateNewGroupRequest dto);
    }

    public class UserService : IUserService
    {
        private readonly UserManager<TsUser> _userManager;
        private readonly SignInManager<TsUser> _signInManager;
        private readonly IUsersRepository _usersRepository;
        private readonly IConfiguration _config;

        public UserService(UserManager<TsUser> userManager,
                           SignInManager<TsUser> signInManager,
                           IUsersRepository usersRepository,
                           IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _usersRepository = usersRepository;
            _config = config;
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
                    res = await _userManager.AddToRoleAsync(createdUser, "Admin");
                    break;
                case UserTypes.Teacher:
                    res = await _userManager.AddToRoleAsync(createdUser, "Teacher");
                    break;
                case UserTypes.Student:
                    res = await _userManager.AddToRoleAsync(createdUser, "Student");
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
                UserName = user.UserName ?? throw new InvelidDataException("UserName cannot be null"),
                Role = roles.First()
            };
        }

        public async Task<List<GetAllGroupsResponseDto>> GetAllGroups()
        {
            return _usersRepository.GetAllGroups();
        }

        public async Task<List<FindUserResponseDto>> FindUser(PaginationRequest<FindRequestDto> paginationRequest)
        {
            return _usersRepository.FindUser(paginationRequest);
        }

        public async Task AddStudentsToGroup(AddStudentsToGroupRequest dto)
        {
            _usersRepository.AddStudentsToGroup(dto);
        }

        public async Task CreateNewGroupAsync(CreateNewGroupRequest dto)
        {
            _usersRepository.CreateNewGroupAsync(dto);
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
