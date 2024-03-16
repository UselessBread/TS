using IdentityService.Data.Contracts.Entities;

namespace IdentityService.Data.Repositories
{
    /// <summary>
    /// Repository for working with Users table
    /// </summary>
    public interface IUsersRepository
    {
        /// <summary>
        /// Get all users as Queryable for joining
        /// </summary>
        /// <returns>All entries as Queryable</returns>
        public IQueryable<TsUser> GetAllUsersAsQuery();
    }

    /// <summary>
    /// Repository for working with Users table
    /// </summary>
    public class UsersRepository : IUsersRepository
    {
        private readonly UsersContext _context;

        public UsersRepository(UsersContext context)
        {
            _context = context;
        }

        /// <inheritdoc/>
        public IQueryable<TsUser> GetAllUsersAsQuery()
        {
            return _context.Users.AsQueryable();
        }
    }
}
