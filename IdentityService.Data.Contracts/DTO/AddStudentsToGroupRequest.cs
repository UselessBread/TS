using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Data.Contracts.DTO
{
    public class AddStudentsToGroupRequest
    {
        public List<Guid> Students { get; set; }
        public Guid GroupImmutableId { get; set; }
    }
}
