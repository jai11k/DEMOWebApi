using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Dtos.Request
{
    public class EditStudentDto : AddStudentDto
    {
        public Guid Id { get; set; }
    }
}
