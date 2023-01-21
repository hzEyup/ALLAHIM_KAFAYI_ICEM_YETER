using AppCore.Business.Services.Bases;
using AppCore.Results;
using AppCore.Results.Bases;
using Business.Models;
using DataAccess.Entities;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore.SqlServer.Design.Internal;
using System.Linq.Expressions;

namespace Business.Services
{
    public interface IStudentService : IService<StudentModel>
    {
        Result DeleteImage(int id);
    }

    public class StudentService : IStudentService
    {
        private readonly StudentRepoBase _studentRepo;

        public StudentService(StudentRepoBase studentRepo)
        {
            _studentRepo = studentRepo;
        }

        public Result Add(StudentModel model)
        {

            if (_studentRepo.Query().Any(p => p.Name.ToUpper() == model.Name.ToUpper().Trim() && p.SurName.ToUpper() == model.SurName.ToUpper().Trim()))
                return new ErrorResult("Student wiht same name Exist!!");
            int schoolNo;
            if (!int.TryParse(model.SchoolNo, out schoolNo))
            {
                return new ErrorResult("School No must be numeric");
            }

            var entity = new Student()
            {
                ClassId = model.ClassId.Value,
                Name = model.Name.Trim(),
                SurName = model.SurName.Trim(),
                SchoolNo = schoolNo,//int.tryparse
                DateOfBirthday = model.DateOfBirthday,
                studentLessons = model.LessonIds?.Select(sId => new StudentLesson()

                {
                    LessonId = sId
                }).ToList()

            };
            _studentRepo.Add(entity);
            return new SuccessResult();
        }

        public Result Delete(int id)
        {
            var studentLessonEntities = _studentRepo.DbContext.Set<StudentLesson>().Where(sl => sl.StudentId == id).ToList();
            _studentRepo.DbContext.Set<StudentLesson>().RemoveRange(studentLessonEntities);
            _studentRepo.DbContext.SaveChanges();

            _studentRepo.Delete(id);
            return new SuccessResult();
        }

        public void Dispose()
        {
            _studentRepo.Dispose();
        }

        public IQueryable<StudentModel> Query()
        {
            return _studentRepo.Query(s => s.Class, s => s.studentLessons).Select(s => new StudentModel()
            {
                ClassId = s.ClassId,
                SchoolNo = s.SchoolNo.ToString(),
                DateOfBirthday = s.DateOfBirthday,
                Guid = s.Guid,
                Id = s.Id,
                Name = s.Name,
                SurName = s.SurName,
                ClassNameDisplay = s.Class.Name,
                LessonDisplay = string.Join("<br/>", s.studentLessons.Select(sl => sl.Lesson.Name)),
                DateOfBirthdayDisplay = s.DateOfBirthday.HasValue ? s.DateOfBirthday.Value.ToString("yyyy/MM/dd") : "",

                Image = s.Image,
                ImgSrcDisplay = s.Image != null ? (s.ImgExtension == ".jpg" ||
                s.ImgExtension == ".jpeg" ? "data:image/jpeg;base64," : "data:image/png;base64,") + Convert.ToBase64String(s.Image) : null
            });
        }

        public Result Update(StudentModel model)
        {
            if (_studentRepo.Query().Any(s => s.Name.ToUpper() == model.Name.ToUpper().Trim() && s.Name != model.Name))
                return new ErrorResult("Student with same name exists");
            int schoolNo;
            if (!int.TryParse(model.SchoolNo, out schoolNo))
            {
                return new ErrorResult("School No must be numeric");
            }
            var studentLessonEntities = _studentRepo.DbContext.Set<StudentLesson>().Where(sl => sl.StudentId == model.Id).ToList();
            _studentRepo.DbContext.Set<StudentLesson>().RemoveRange(studentLessonEntities);
            _studentRepo.DbContext.SaveChanges();

            var entity = _studentRepo.Query().SingleOrDefault(s => s.Id == model.Id);

            entity.ClassId = model.ClassId.Value;
            entity.DateOfBirthday = model.DateOfBirthday;
            entity.Name = model.Name.Trim();
            entity.SurName = model.SurName.Trim();
            entity.SchoolNo = schoolNo;
            entity.Id = model.Id;
            entity.studentLessons = model.LessonIds?.Select(lId => new StudentLesson()
            {
                LessonId = lId
            }).ToList();

  
            
            if (model.Image != null)
            {
                entity.Image = model.Image;
                entity.ImgExtension = model.ImgExtension?.ToLower();
            }
            _studentRepo.Update(entity);
            return new SuccessResult();
        }
        public Result DeleteImage(int id)
        {
            var student =  _studentRepo.Query(s =>s.Id == id).SingleOrDefault();
            student.Image = null;
            student.ImgExtension = null;
            _studentRepo.Update(student);
            return new SuccessResult();
        }
    }
}
