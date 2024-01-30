using Common.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using TS.Data.Contracts.DTO;

namespace TS.Data.Contracts.Entities
{
    public class TestsContent : VersionedEntityBase
    {
        public int Id { get; set; }

        [Column(TypeName = "jsonb")]
        public List<TaskDto> Tasks { get; set; } = new List<TaskDto>();
    }
}