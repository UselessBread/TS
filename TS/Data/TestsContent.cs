using System.ComponentModel.DataAnnotations.Schema;
using TS.DTO;

namespace TS.Data
{
    public class TestsContent : VersionedEntityBase
    {
        public int Id { get; set; }
        public int TestDescriptionId { get; set; }
        public Guid TestDescriptionImmutableId { get; set; }

        [Column(TypeName = "jsonb")]
        public List<TaskDto> Tasks { get; set; } = new List<TaskDto>();
    }
}