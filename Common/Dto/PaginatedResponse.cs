using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dto
{
    public class PaginatedResponse<T>
    {
        public List<T> Result { get;set; }
        public int AllEntriesCount { get; set; }

        public PaginatedResponse(List<T> results, int allEntriesCount) 
        { 
            Result = results;
            AllEntriesCount = allEntriesCount;
        }

    }
}
