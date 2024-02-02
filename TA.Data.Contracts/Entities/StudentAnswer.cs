using System.ComponentModel.DataAnnotations.Schema;
using TA.Data.Contracts.Dto;

namespace TA.Data.Contracts.Entities
{
    public class StudentAnswer
    {
        public int Id { get; set; }
        public Guid AssignedTestImmutableId {  get; set; }
        public Guid UserId { get; set; }
        [Column(TypeName = "jsonb")]
        public List<Answer> Answers {  get; set; }
    }
}
