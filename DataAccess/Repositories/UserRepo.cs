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
    public abstract class UserRepoBase : RepoBase<User>
    {
        protected UserRepoBase(StudentKayitContext dbContext) : base(dbContext)
        {

        }

        
    }
    public class UserRepo : UserRepoBase
    {
        public UserRepo(StudentKayitContext dbContext) : base(dbContext)
        {

        }
    }

}
