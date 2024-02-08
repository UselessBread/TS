using Common.Constants;

namespace TA.Data.Contracts.Dto
{
    public class AssisgnedTestResponseDto
    {
        public Guid AssignmentImmutableId { get; set; }
        public Guid TeacherId { get; set; }
        public int TestDescriptionId { get; set; }
        public DateTime AssignedTime { get; set; }
        public DateTime DueTo { get; set; }
        public AssignedTestState State { get; set; }
    }
}
