using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Data.Contracts.DTO
{
    public class CreateNewGroupRequest
    {
        public string Name { get; set; }
        public Guid TeacherId { get; set; }
    }
}
