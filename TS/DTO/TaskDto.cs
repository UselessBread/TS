namespace TS.DTO
{
    public class TaskDto
    {
        public int Position { get; set; }
        public string TaskDescription { get; set; } = string.Empty;
        public TestTypes Type { get; set; }
        public List<string>? Answers { get; set; }
        public List<int>? RightAnswers { get; set; }
    }
}