namespace TA.Data.Contracts.Dto
{
    public class AssignedTestReviewSaveRequestDto
    {
        public Guid AssignedTestImmutableId { get; set; }
        public int StudentAnswerId {  get; set; }
        public List<Comments> Comments { get; set; }
        public string FinalComment { get; set; }
    }
}
