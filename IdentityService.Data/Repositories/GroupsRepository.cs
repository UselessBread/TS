using Common.Dto;
using Common.Exceptions;
using Common.Extensions;
using IdentityService.Data.Contracts.DTO;
using IdentityService.Data.Contracts.Entities;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Data.Repositories
{
    /// <summary>
    /// Repository for wirking with Groups table
    /// </summary>
    public interface IGroupsRepository
    {
        /// <summary>
        /// Get all existing groups
        /// </summary>
        /// <param name="paginationRequest">pagination info</param>
        /// <returns>paginated response</returns>
        public Task<PaginatedResponse<GetAllGroupsResponseDto>> GetAllGroupsAsync(PaginationRequest paginationRequest);

        /// <summary>
        /// Get actual group by immutable Id
        /// </summary>
        /// <param name="immutableId">Immutable id of the group</param>
        /// <returns>found result</returns>
        public Task<Groups> GetByImmutableId(Guid immutableId);

        /// <summary>
        /// Create a new group
        /// </summary>
        /// <param name="dto">Info for the new group</param>
        /// <returns>ImmutableId of the created group</returns>
        public Task<Guid> CreateNewGroup(CreateNewGroupRequest dto);

        /// <summary>
        /// Update group
        /// </summary>
        /// <param name="dto">Info to update</param>
        /// <returns></returns>
        public Task UpdateGroup(UpdateGroupRequestDto dto);

        /// <summary>
        /// Check if the specified group exists
        /// </summary>
        /// <param name="immutableId">Desired immutable id of the group</param>
        /// <returns>true if exists</returns>
        public Task<bool> IsExists(Guid immutableId);
    }

    /// <summary>
    /// Repository for wirking with Groups table
    /// </summary>
    public class GroupsRepository : IGroupsRepository
    {
        private readonly UsersContext _context;
        public GroupsRepository(UsersContext context)
        {
            _context = context;
        }

        /// <inheritdoc/>
        public async Task<PaginatedResponse<GetAllGroupsResponseDto>> GetAllGroupsAsync(PaginationRequest paginationRequest)
        {
            IQueryable<GetAllGroupsResponseDto> request = _context.Groups.Join(_context.Users, g => g.TeacherId, u => u.Id, (g, u) => new
            {
                GroupImmutableId = g.ImmutableId,
                GroupName = g.Name,
                TeacherName = u.Name,
                TeacherSurname = u.Surname,
                DeletionDate = g.DeletionDate
            }).Where(g => g.DeletionDate == null).Select(r => new GetAllGroupsResponseDto
            {
                GroupName = r.GroupName,
                TeacherName = r.TeacherName,
                TeacherSurname = r.TeacherSurname,
                GroupImmutableId = r.GroupImmutableId
            });

            return await request.PaginateResult(paginationRequest);
        }

        /// <inheritdoc/>
        public async Task<Groups> GetByImmutableId(Guid immutableId)
        {
            return await _context.Groups.FirstOrDefaultAsync(g => g.ImmutableId == immutableId && g.DeletionDate == null)
                ?? throw new EntityNotFoundException($"Entity with UUID {immutableId} was not found");
        }

        /// <inheritdoc/>
        public async Task<Guid> CreateNewGroup(CreateNewGroupRequest dto)
        {
            Guid groupImmutableId = Guid.NewGuid();
            _context.Groups.Add(new Groups
            {
                ImmutableId = groupImmutableId,
                Name = dto.Name,
                TeacherId = dto.TeacherId,
                Version = 1,
                CreationDate = DateTime.Now.ToUniversalTime(),
            });

            await _context.SaveChangesAsync();

            return groupImmutableId;
        }

        /// <inheritdoc/>
        public async Task UpdateGroup(UpdateGroupRequestDto dto)
        {
            Groups res = _context.Groups.FirstOrDefault(g => g.ImmutableId == dto.GroupImmutableId && g.DeletionDate == null)
                ?? throw new EntityNotFoundException($"Entity with UUID {dto.GroupImmutableId} was not found");

            DateTime currentTime = DateTime.Now.ToUniversalTime();
            res.DeletionDate = currentTime;
            _context.Groups.Update(res);

            Groups createdGroup = new Groups
            {
                ImmutableId = res.ImmutableId,
                Version = res.Version + 1,
                Name = dto.Name,
                TeacherId = dto.TeacherId,
                CreationDate = currentTime
            };
            _context.Groups.Add(createdGroup);

            await _context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task<bool> IsExists(Guid immutableId)
        {
            return await _context.Groups.AnyAsync(g => g.ImmutableId == immutableId);
        }
    }
}
