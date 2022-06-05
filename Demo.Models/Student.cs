using Demo.Common;
using Demo.Models.Base;

namespace Demo.Models
{
    public class Student : ModelEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Standard Standard { get; set; }
    }
}