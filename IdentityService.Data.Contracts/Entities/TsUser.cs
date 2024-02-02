using Microsoft.AspNetCore.Identity;

namespace IdentityService.Data.Contracts.Entities
{
    public class TsUser : IdentityUser<Guid>
    {
        public required string Name { get; set; }
        public required string Surname { get; set; }

    }
}
