namespace IdentityService.Data.Contracts.DTO
{
    public class UpdateGroupRequestDto
    {
        public string Name { get; set; }
        public Guid TeacherId { get; set; }
        public Guid GroupImmutableId { get; set; }
    }
}
