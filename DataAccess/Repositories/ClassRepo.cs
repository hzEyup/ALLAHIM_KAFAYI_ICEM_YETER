using AppCore.DataAccess.EntityFramework.Bases;
using DataAccess.Contexts;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public abstract class ClassRepoBase : RepoBase<Class>
    {
        protected ClassRepoBase(StudentKayitContext dbContext) : base(dbContext)
        {
        }
    }

    public class ClassRepo : ClassRepoBase
    {
        public ClassRepo(StudentKayitContext dbContext) : base(dbContext)
        {
        }
    }
}
