using AppCore.DataAccess.EntityFramework.Bases;
using DataAccess.Contexts;
using DataAccess.Entities;
using System.Linq.Expressions;

namespace DataAccess.Repositories
{
    public abstract class LessonRepoBase : RepoBase<Lesson>
    {
        protected LessonRepoBase(StudentKayitContext dbContext) : base(dbContext)
        {
        }
    }

    public class LessonRepo : LessonRepoBase
    {
        public LessonRepo(StudentKayitContext dbContext) : base(dbContext)
        {
        }
        public override IQueryable<Lesson> Query(params Expression<Func<Lesson, object?>>[] entitiesToInclude)
        {
            return base.Query(entitiesToInclude).Where(q => !q.IsDeleted);
        }

        public override void Delete(Lesson entity, bool save = true)
        {
            entity.IsDeleted = true;
            base.Delete(entity, save);
        }
    }
}
