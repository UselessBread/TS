namespace TS.DTO
{
    public class UpdateTestDto
    {
        public string TestName { get; set; }
        public List<TaskDto> Tasks { get; set; }
        public Guid TestDescriptionImmutableId { get; set; }
        public Guid TestContentImmutableId { get; set; }
    }
}