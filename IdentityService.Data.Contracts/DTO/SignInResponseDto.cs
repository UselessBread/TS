namespace IdentityService.Data.Contracts.DTO
{
    public class SignInResponseDto
    {
        public string Token { get; set; }
        public string UserName {  get; set; }
        public string Role {  get; set; }
    }
}
