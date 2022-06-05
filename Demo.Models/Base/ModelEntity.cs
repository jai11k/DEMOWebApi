using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Models.Base
{
    public class ModelEntity
    {
        public ModelEntity()
        {
            Id=Guid.NewGuid();
            CreatedDate=DateTime.Now;
            IsDeleted = false;
        }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool  IsDeleted { get; set; }
        public Guid Id { get; set; }
    }
}
