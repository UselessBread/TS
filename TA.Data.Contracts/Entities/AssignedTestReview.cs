using System.ComponentModel.DataAnnotations.Schema;
using TA.Data.Contracts.Dto;

namespace TA.Data.Contracts.Entities
{
    public class AssignedTestReview
    {
        public int Id { get; set; }
        public int StudentAnswerId { get; set; }
        public Guid AssignedTestImmutableId { get; set; }
        [Column(TypeName = "jsonb")]
        public List<Comments> Comments { get; set; }
        public string FinalComment { get; set; }
    }
}
