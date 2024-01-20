namespace TS.Data.Contracts.DTO
{
    public class CreateNewTestDto
    {
        public string TestName { get; set; }
        public List<TaskDto> Tasks { get; set; }
    }
}