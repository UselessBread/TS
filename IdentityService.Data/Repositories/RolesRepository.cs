using Microsoft.AspNetCore.Identity;

namespace IdentityService.Data.Repositories
{
    /// <summary>
    /// Repository for working with Roles repository
    /// </summary>
    public interface IRolesRepository
    {
        /// <summary>
        /// Get All roles as Queryable for joining
        /// </summary>
        /// <returns>All entries as queriable</returns>
        public IQueryable<IdentityRole<Guid>> GetAllRolesAsQuery();
    }

    /// <summary>
    /// Repository for working with Roles repository
    /// </summary>
    public class RolesRepository : IRolesRepository
    {
        private readonly UsersContext _context;

        public RolesRepository(UsersContext context)
        {
            _context = context;
        }

        /// <inheritdoc/>
        public IQueryable<IdentityRole<Guid>> GetAllRolesAsQuery()
        {
            return _context.Roles.AsQueryable();
        }
    }
}
