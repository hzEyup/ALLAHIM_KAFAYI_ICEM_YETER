using AppCore.Business.Services.Bases;
using AppCore.Results;
using AppCore.Results.Bases;
using Business.Models;
using DataAccess.Entities;
using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public interface IClassService : IService<ClassModel>
    {
    }

    public class ClassService : IClassService
    {
        private readonly ClassRepoBase _classRepo;

        public ClassService(ClassRepoBase classRepo)
        {
            _classRepo = classRepo;
        }

        public Result Add(ClassModel model)
        {
            {
                if (Query().Any(c => c.Name.ToUpper() == model.Name.ToUpper().Trim()))
                    return new ErrorResult("Class with same name exists!");
                var entity = new Class()
                {
                    Name = model.Name.Trim(),
                };
                _classRepo.Add(entity);
                return new SuccessResult("Class created successfully.");
            }
        }

        public Result Delete(int id)
        {
            var Class = Query().SingleOrDefault(c => c.Id == id);
            if (Class.StudentCountDisplay > 0)
                return new ErrorResult("You cant deleted the class because it has students!");
            _classRepo.Delete(id);
            return new SuccessResult("Class deleted is Successfully");
        }

        public void Dispose()
        {
            _classRepo.Dispose();
        }

        public IQueryable<ClassModel> Query()
        {
            return _classRepo.Query().Select(s => new ClassModel()
            {
                Guid = s.Guid,
                Name = s.Name,
                Id = s.Id,
                StudentCountDisplay=s.Students.Count

            });
        }

        public Result Update(ClassModel model)
        {
            if (Query().Any(c => c.Name.ToUpper() == model.Name.ToUpper().Trim() && c.Id != model.Id))
                return new ErrorResult("Class with same name exist!");
            var entity = new Class()
            {
                Name = model.Name.Trim(),
            };
            _classRepo.Update(entity);
            return new SuccessResult("Class updated succesfully.");
        }
    }
}
