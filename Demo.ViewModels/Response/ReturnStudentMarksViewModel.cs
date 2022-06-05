using Demo.Common;
using Demo.ViewModels.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.ViewModels.Response
{
    public class ReturnStudentMarksViewModel
    {
        public EditStudentViewModel Student { get; set; }
        public Subject Subject { get; set; }
        public int Marks { get; set; }
    }
}
