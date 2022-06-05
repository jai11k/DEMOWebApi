using Demo.Common;
using Demo.Models.Base;

namespace Demo.Models
{
    public class StudentMarks : ModelEntity
    {
        public Guid StudentId { get; set; }
        public Subject Subject { get; set; }
        public int Marks { get; set; }
    }
}
