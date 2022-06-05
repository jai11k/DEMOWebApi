using Demo.Common;
using Demo.Dtos.Request;
using Demo.Dtos.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Services.Interface
{
    public interface IStudentService
    {
         
        Task<Guid> AddStudent(AddStudentDto registerStudentDto);
        Task<Guid> EditStudent(EditStudentDto editStudentDto);
        Task<Guid> DeleteStudent(DeleteEntityDto deleteEntityDto);

        Task<Guid> AddStudentAndMarks(AddStudentAndMarksDto addStudentAndMarksDto);

        Task<MultipleRecordsAndCount<IEnumerable<ReturnStudentAndMarksDto>>> GetAllStudentAndTheirMarks();
        Task<MultipleRecordsAndCount<IEnumerable<ReturnStudentMarksDto>>> GetMultipleStudentRecords(GetFilteredRecordsDto getStudenttRecordsDto);

    }
}
