using Demo.Models;
using Microsoft.EntityFrameworkCore;

namespace Demo.Db.DataContext
{
    public class AppDbContext : DbContext,IAppDbContext
    {
        //public AppDbContext()
        //{

        //}
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public async Task<int> Save(CancellationToken cancellationToken)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }

        public async Task<int> Save(bool acceptAllChangesOnSuccess)
        {
            return await base.SaveChangesAsync(acceptAllChangesOnSuccess);
        }

        public async Task<int> Save()
        {
            return await base.SaveChangesAsync();
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<StudentMarks> StudentMarks { get; set; }
    }
}