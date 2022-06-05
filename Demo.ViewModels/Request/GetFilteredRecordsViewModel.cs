using Demo.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.ViewModels.Request
{
    public class GetFilteredRecordsViewModel
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public SearchFilterViewModel? SearchFilter { get; set; }
    }
}
