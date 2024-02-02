using Common.Entities;

namespace IdentityService.Data.Contracts.Entities
{
    public class StudentsByGroups
    {
        public int Id {  get; set; }
        public Guid GroupImmutableId { get; set; }
        public Guid StuedntId { get; set; }
    }
}
