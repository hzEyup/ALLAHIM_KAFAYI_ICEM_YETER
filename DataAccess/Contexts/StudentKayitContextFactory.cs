using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DataAccess.Contexts
{
    public class StudentKayitContextFactory : IDesignTimeDbContextFactory<StudentKayitContext>
    {
        public StudentKayitContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<StudentKayitContext>();
            optionsBuilder.UseSqlServer("server=.\\SQLEXPRESS;database=StudentSystem;user id=sa;password=sa;multipleactiveresultsets=true;trustservercertificate=true;");
            return new StudentKayitContext(optionsBuilder.Options);
        }
    }
}