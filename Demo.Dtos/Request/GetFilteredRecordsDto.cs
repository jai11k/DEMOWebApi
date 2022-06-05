using Demo.Common;
using Demo.Dtos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Dtos.Request
{
    public class GetFilteredRecordsDto
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public SearchFilterDto? SearchFilter { get; set; }

    }
}
