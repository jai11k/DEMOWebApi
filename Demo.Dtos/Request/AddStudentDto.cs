using Demo.Common;

namespace Demo.Dtos.Request
{
    public class AddStudentDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Standard Standard { get; set; }
    }
}