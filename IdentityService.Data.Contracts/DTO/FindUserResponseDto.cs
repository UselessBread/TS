namespace IdentityService.Data.Contracts.DTO
{
    public class FindUserResponseDto
    {
        public string Name {  get; set; }
        public string Surname {  get; set; }
        public string Email { get; set; }
        public Guid UserId { get; set; }
    }
}
