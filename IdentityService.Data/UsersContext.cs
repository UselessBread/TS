using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Data
{
    public class UsersContext:IdentityDbContext<IdentityUser>
    {
        public UsersContext(DbContextOptions<UsersContext> options) :
        base(options)
        { }

    }
}
