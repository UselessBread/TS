using System.ComponentModel.DataAnnotations.Schema;

namespace TS.Data
{
    public class TestsContent : VersionedEntityBase
    {
        public int Id { get; set; }
        public int TestDescriptionId { get; set; }
        public Guid TestDescriptionImmutableId { get; set; }
        public TestTypes Type { get; set; }

        [Column(TypeName = "jsonb")]
        public object Tasks { get; set; } = new object();
    }
}