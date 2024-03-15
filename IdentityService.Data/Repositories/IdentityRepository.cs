using Common.Dto;
using Common.Exceptions;
using Common.Extensions;
using IdentityService.Data.Contracts.DTO;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Data.Repositories
{
    /// <summary>
    /// Repository for identity tables. Shitty solution
    /// </summary>
    public interface IIdentityRepository
    {
        /// <summary>
        /// Find user by a custom request
        /// </summary>
        /// <param name="paginationRequest">pagination request with a request for searching user</param>
        /// <returns>paginated response of found users</returns>
        public Task<PaginatedResponse<FindUserResponseDto>> FindUserAsync(PaginationRequest<FindRequestDto> paginationRequest);

        /// <summary>
        /// Check if user has a role
        /// </summary>
        /// <param name="userId">Id of the user to check</param>
        /// <param name="roleName">Name of the role that it shoud have</param>
        /// <returns>true if it has specified role</returns>
        public Task<bool> CheckIfHasRole(Guid userId, string roleName);
    }

    /// <summary>
    /// Repository for identity tables. Shitty solution
    /// </summary>
    public class IdentityRepository : IIdentityRepository
    {
        private readonly UsersContext _context;
        
        public IdentityRepository(UsersContext context)
        {
            _context = context;
        }

        /// <inheritdoc/>
        public async Task<PaginatedResponse<FindUserResponseDto>> FindUserAsync(PaginationRequest<FindRequestDto> paginationRequest)
        {
            FindRequestDto requestDto = paginationRequest.Request;
            string roleToBeFound = string.Empty;
            switch (requestDto.UserType)
            {
                case Common.Constants.UserTypes.Admin:
                    roleToBeFound = "Admin";
                    break;
                case Common.Constants.UserTypes.Teacher:
                    roleToBeFound = "Teacher";
                    break;
                case Common.Constants.UserTypes.Student:
                    roleToBeFound = "Student";
                    break;
                default:
                    throw new BadRequestException("Unsupported User type");
            }

            var joinedTables = _context.UserRoles.Join(_context.Users, ur => ur.UserId, u => u.Id, (ur, u) => new
            {
                User = u,
                RoleId = ur.RoleId,
            }).Join(_context.Roles, res => res.RoleId, r => r.Id, (res, r) => new
            {
                User = res.User,
                Role = r
            }).Where(res => res.Role.Name == roleToBeFound);

            if (!string.IsNullOrEmpty(requestDto.Name))
                joinedTables = joinedTables.Where(r => r.User.Name.Contains(requestDto.Name));

            if (!string.IsNullOrEmpty(requestDto.Surname))
                joinedTables = joinedTables.Where(r => r.User.Surname.Contains(requestDto.Surname));

            return await joinedTables.Select(res => new FindUserResponseDto
            {
                UserId = res.User.Id,
                Surname = res.User.Surname,
                Email = res.User.Email ?? string.Empty,
                Name = res.User.Name
            }).PaginateResult(paginationRequest);
        }

        /// <inheritdoc/>
        public async Task<bool> CheckIfHasRole(Guid userId, string roleName)
        {
            // check for teacher
            if (!await _context.UserRoles.Join(_context.Roles, ur => ur.RoleId, r => r.Id, (ur, r) => new
            {
                UserId = ur.UserId,
                RoleName = r.Name
            }).AnyAsync(res => res.UserId == userId && res.RoleName == roleName))
                return false;

            return true;
        }
    }
}
