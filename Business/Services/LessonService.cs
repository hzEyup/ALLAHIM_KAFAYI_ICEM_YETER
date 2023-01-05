using AppCore.Business.Services.Bases;
using AppCore.Results.Bases;
using AppCore.Results;
using Business.Models;
using DataAccess.Entities;
using DataAccess.Repositories;

namespace Business.Services
{
    public interface ILessonService : IService<LessonModel>
    {
    }

    public class LessonService : ILessonService
    {
        private readonly LessonRepoBase _lessonRepo;

        public LessonService(LessonRepoBase lessonRepo)
        {
            _lessonRepo = lessonRepo;
        }

        public Result Add(LessonModel model)
        {
            if (Query().Any(s => s.Name.ToUpper() == model.Name.ToUpper().Trim()))
                return new ErrorResult("Lesson can't be added because lesson with the same name exists!");
            var entity = new Lesson()
            {
                IsNumeric = model.IsNumeric,
                Name = model.Name.Trim()
            };
            _lessonRepo.Add(entity);
            return new SuccessResult();
        }

        public Result Delete(int id)
        {
            _lessonRepo.Delete(id);
            return new SuccessResult();
        }

        public void Dispose()
        {
            _lessonRepo.Dispose();
        }

        public IQueryable<LessonModel> Query()
        {
            return _lessonRepo.Query().OrderBy(s => s.IsNumeric).ThenBy(s => s.Name).Select(s => new LessonModel()
            {
                Guid = s.Guid,
                Id = s.Id,
                Name = s.Name,
                IsNumeric = s.IsNumeric,
                NumericalDisplay = s.IsNumeric ? "Yes" : "No"
            });
        }

        public Result Update(LessonModel model)
        {
            if (Query().Any(s => s.Name.ToUpper() == model.Name.ToUpper().Trim() && s.Id != model.Id))
                return new ErrorResult("Lesson can't be added because lesson with the same name exists!");
            var entity = new Lesson()
            {
                Id = model.Id,
                IsNumeric = model.IsNumeric,
                Name = model.Name.Trim()
            };
            _lessonRepo.Update(entity);
            return new SuccessResult();
        }
    }
}
