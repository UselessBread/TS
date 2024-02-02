namespace IdentityService.Data.Contracts.DTO
{
    public class GetGroupInfoResponseDto
    {
        public required string GroupName {  get; set; }
        public Guid TeacherId { get; set; }
        public List<Guid> StudentIds { get; set; } = new List<Guid>();
    }
}
