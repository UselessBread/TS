using Common.Entities;

namespace IdentityService.Data.Contracts.Entities
{
    public class Groups : VersionedEntityBase
    {
        public int Id {  get; set; }
        public string Name {  get; set; }
        public Guid TeacherId { get; set; }
    }
}
