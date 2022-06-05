using Demo.Dtos.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Services.Interface
{
    public interface IStudentMarksService
    {

        Task<Guid> AddStudentMarks(AddStudentMarksDto registerStudentDto);
        Task<Guid> EditStudentMarks(EditStudentMarksDto editStudentDto);
        Task<Guid> DeleteStudentMarks(DeleteEntityDto deleteEntityDto);
    }
}
