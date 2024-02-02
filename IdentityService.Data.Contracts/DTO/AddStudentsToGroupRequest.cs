namespace IdentityService.Data.Contracts.DTO
{
    public class AddStudentsToGroupRequest
    {
        public List<Guid> Students { get; set; }
        public Guid GroupImmutableId { get; set; }
    }
}
