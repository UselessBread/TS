using Common.Constants;
using Common.Entities;

namespace TA.Data.Contracts.Entities
{
    public class AssignedTests : VersionedEntityBase
    {
        public int Id { get; set; }
        public int TestDescriptionId { get; set; }
        public Guid? GroupImmutableId { get; set; }  
        public Guid? StudentImmutableId { get; set; }
        public DateTime AssignedTime { get; set; }
        public DateTime DueTo {  get; set; }
        public AssignedTestState State { get; set; }
    }
}
