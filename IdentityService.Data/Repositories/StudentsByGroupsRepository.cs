using IdentityService.Data.Contracts.DTO;
using IdentityService.Data.Contracts.Entities;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Data.Repositories
{
    /// <summary>
    /// Repository for working with StudentsByGroups table
    /// </summary>
    public interface IStudentsByGroupsRepository
    {
        /// <summary>
        /// Add stidents to group
        /// </summary>
        /// <param name="dto">info for operation</param>
        /// <returns></returns>
        public Task AddStudentsToGroup(AddStudentsToGroupRequest dto);

        /// <summary>
        /// Get grups, in which specified user exists
        /// </summary>
        /// <param name="userId">id of the user</param>
        /// <returns>ImmutableIds of the groups</returns>
        public Task<List<Guid>> GetGroupsForUser(Guid userId);

        /// <summary>
        /// Get all users in a specified group
        /// </summary>
        /// <param name="groupImmutableId">ImmutableId of the group</param>
        /// <returns>Ids of the users</returns>
        public Task<List<Guid>> GetUsersInGroup(Guid groupImmutableId);
    }

    /// <summary>
    /// Repository for working with StudentsByGroups table
    /// </summary>
    public class StudentsByGroupsRepository: IStudentsByGroupsRepository
    {
        private readonly UsersContext _context;
        public StudentsByGroupsRepository(UsersContext context)
        {
            _context = context;
        }

        /// <inheritdoc/>
        public async Task AddStudentsToGroup(AddStudentsToGroupRequest dto)
        {
            DateTime creationDate = DateTime.Now.ToUniversalTime();

            // only add new students
            var existingStuds = _context.StudentsByGroups
                .Where(g => g.GroupImmutableId == dto.GroupImmutableId && dto.Students.Contains(g.StuedntId))
                .Select(res => res.StuedntId).ToList();
            var studentsToAdd = dto.Students.Where(s => !existingStuds.Contains(s)).ToList();
            var studentsToDelete = _context.StudentsByGroups
                .Where(g => g.GroupImmutableId == dto.GroupImmutableId && !dto.Students.Contains(g.StuedntId)).ToArray();

            foreach (Guid student in studentsToAdd)
            {
                _context.StudentsByGroups.Add(new StudentsByGroups
                {
                    StuedntId = student,
                    GroupImmutableId = dto.GroupImmutableId,
                });
            }

            _context.StudentsByGroups.RemoveRange(studentsToDelete);
            await _context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task<List<Guid>> GetGroupsForUser(Guid userId)
        {
            return await _context.StudentsByGroups.Where(g => g.StuedntId == userId).Select(res => res.GroupImmutableId).ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<List<Guid>> GetUsersInGroup(Guid groupImmutableId)
        {
            return await _context.StudentsByGroups.Where(sg => sg.GroupImmutableId == groupImmutableId)
                .Select(r => r.StuedntId)
                .ToListAsync();
        }
    }
}
