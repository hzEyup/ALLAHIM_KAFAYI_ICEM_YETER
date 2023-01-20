using AppCore.DataAccess.EntityFramework.Bases;
using DataAccess.Contexts;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public abstract class CountryRepoBase : RepoBase<Country>
    {
        protected CountryRepoBase(StudentKayitContext dbContext) : base(dbContext)
        {
        }
    }

    public class CountryRepo : CountryRepoBase
    {
        public CountryRepo(StudentKayitContext dbContext) : base(dbContext)
        {
        }
    }
}
