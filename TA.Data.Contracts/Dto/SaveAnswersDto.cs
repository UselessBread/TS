namespace TA.Data.Contracts.Dto
{
    public class SaveAnswersDto
    {
        public required Guid AssignedTestImmutableId { get; set; }
        public required List<Answer> Tasks { get; set; }
    }
}
