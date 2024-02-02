using Common.Constants;

namespace IdentityService.Data.Contracts.DTO
{
    public class CreateUserRequestDto
    {
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public required string Email { get; set; }
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public UserTypes UserType {  get; set; }
    }
}
