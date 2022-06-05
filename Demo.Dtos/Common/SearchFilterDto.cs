using Demo.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Dtos.Common
{
    public class SearchFilterDto
    {
        public int MarksAbove { get; set; }
        public Subject Subject { get; set; }
        public Standard Standard { get; set; }
        public string Name { get; set; }
    }
}
