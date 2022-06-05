using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.ViewModels.Request
{
    public class EditStudentViewModel : AddStudentViewModel
    {
        public Guid Id { get; set; }
    }
}
