using Common.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Data.Contracts.DTO
{
    public class FindRequestDto
    {
        public string? Name {  get; set; }
        public string? Surname { get; set; }
        public UserTypes UserType { get; set; }
    }
}
