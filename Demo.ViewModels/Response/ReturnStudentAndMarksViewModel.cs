using Demo.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.ViewModels.Response
{
    public class ReturnStudentAndMarksViewModel
    {
        public Guid StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Standard Standard { get; set; }
        public Subject Subject { get; set; }
        public int Marks { get; set; }
    }
}
