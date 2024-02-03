using Common.Entities;

namespace TS.Data.Contracts.Entities
{
    public class TestDescriptions : VersionedEntityBase
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int TestContentId { get; set; }
        public Guid TestContentImmutableId { get; set; }
        public Guid CrreatedBy { get; set; }
    }
}