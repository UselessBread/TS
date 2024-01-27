using Common.Constants;

namespace IdentityService.Data.Contracts.DTO
{
    public class CreateUserRequestDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public UserTypes UserType {  get; set; }
    }
}
