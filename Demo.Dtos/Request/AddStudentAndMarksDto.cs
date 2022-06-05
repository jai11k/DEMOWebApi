using Demo.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Dtos.Request
{
    public class AddStudentAndMarksDto
    {
        public AddStudentDto Student { get; set; }
        public Subject Subject { get; set; }
        public int Marks { get; set; }
    }
}
