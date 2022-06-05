using Demo.Common;
using Demo.Dtos.Request;

namespace Demo.Dtos.Response
{
    public class ReturnStudentMarksDto
    {
        public EditStudentDto Student { get; set; }
        public Subject Subject { get; set; }
        public int Marks { get; set; }
    }
}
