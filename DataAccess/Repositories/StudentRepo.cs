using AppCore.DataAccess.EntityFramework.Bases;
using DataAccess.Contexts;
using DataAccess.Entities;

namespace DataAccess.Repositories
{
    public abstract class StudentRepoBase : RepoBase<Student>
    {
        protected StudentRepoBase(StudentKayitContext dbContext) : base(dbContext)
        {
        }
    }

    public class StudentRepo : StudentRepoBase
    {
        public StudentRepo(StudentKayitContext dbContext) : base(dbContext)
        {
        }
    }
}
