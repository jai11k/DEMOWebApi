using Demo.Common;

namespace Demo.Dtos.Request
{
    public class AddStudentMarksDto
    {
        public Guid StudentId { get; set; }
        public Subject Subject { get; set; }
        public int Marks { get; set; }
    }
}
