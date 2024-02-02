namespace TA.Data.Contracts.Dto
{
    public class AssignTestRequestDto
    {
        public int TestDescriptionId { get; set; }
        public Guid? GroupImmutableId { get; set; }
        public Guid? StudentImmutableId { get; set; }
    }
}
