namespace TA.Data.Contracts.Dto
{
    public class Answer
    {
        public required int Position { get; set; }
        public required List<string> Answers { get; set; }
    }
}
