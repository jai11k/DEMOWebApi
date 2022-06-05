using Demo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Db.DataContext
{
    public interface IAppDbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentMarks> StudentMarks { get; set; }
        Task<int> Save(CancellationToken cancellationToken);
        Task<int> Save(bool acceptAllChangesOnSuccess);
        Task<int> Save();
    }
}
