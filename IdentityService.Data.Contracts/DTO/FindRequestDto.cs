using Common.Constants;

namespace IdentityService.Data.Contracts.DTO
{
    public class FindRequestDto
    {
        public string? Name {  get; set; }
        public string? Surname { get; set; }
        public UserTypes UserType { get; set; }
    }
}
