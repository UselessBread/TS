namespace IdentityService.Data.Contracts.DTO
{
    public class GetAllGroupsResponseDto
    {
        public Guid GroupImmutableId {  get; set; }
        public string GroupName { get; set; }
        public string TeacherName { get; set; }
        public string TeacherSurname { get; set; }
    }
}
