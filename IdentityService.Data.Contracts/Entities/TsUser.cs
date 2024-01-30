using Microsoft.AspNetCore.Identity;

namespace IdentityService.Data.Contracts.Entities
{
    public class TsUser : IdentityUser<Guid>
    {
        public string? Name {  get; set; } = null;
        public string? Surname { get; set; } = null;

    }
}
