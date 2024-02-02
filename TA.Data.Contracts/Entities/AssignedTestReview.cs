using System.ComponentModel.DataAnnotations.Schema;
using TA.Data.Contracts.Dto;

namespace TA.Data.Contracts.Entities
{
    public class AssignedTestReview
    {
        public int Id { get; set; }
        public Guid AssignedTestImmutableId { get; set; }
        [Column(TypeName = "jsonb")]
        public List<Comments> Comments { get; set; }
        [Column(TypeName = "jsonb")]
        public List<Correction> Corrections { get; set; }
    }
}
