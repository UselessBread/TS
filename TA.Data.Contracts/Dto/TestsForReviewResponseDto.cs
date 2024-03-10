using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TA.Data.Contracts.Dto
{
    public class TestsForReviewResponseDto
    {
        public int Id { get; set; }
        public Guid AssignedTestImmutableId { get; set; }
        public Guid UserId { get; set; }
        [Column(TypeName = "jsonb")]
        public List<Answer> Answers { get; set; }

    }
}
