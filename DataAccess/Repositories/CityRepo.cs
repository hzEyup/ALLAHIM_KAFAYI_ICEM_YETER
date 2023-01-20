using AppCore.DataAccess.EntityFramework.Bases;
using DataAccess.Contexts;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public abstract class CityRepoBase : RepoBase<City>
    {
        protected CityRepoBase(StudentKayitContext dbContext) : base(dbContext)
        {
        }
    }

    public class CityRepo : CityRepoBase
    {
        public CityRepo(StudentKayitContext dbContext) : base(dbContext)
        {
        }
    }
}
