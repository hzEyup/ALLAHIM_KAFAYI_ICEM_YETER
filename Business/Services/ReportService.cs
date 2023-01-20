using Business.Models;
using DataAccess.Entities;
using DataAccess.Repositories;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Business.Services
{
    public interface IReportService
    {
        List<ReportModel> GetListInnerJoin(ReportFilterModel filter);
    }


    public class ReportService : IReportService
    {
        private readonly StudentRepoBase _studentRepo;

        public ReportService(StudentRepoBase studentRepo)
        {
            _studentRepo = studentRepo;
        }


        public List<ReportModel> GetListInnerJoin(ReportFilterModel filter)
        {
            var studentQuery = _studentRepo.Query();
            var classQuery = _studentRepo.Query<Class>();
            var lessonQuery = _studentRepo.Query<Lesson>();
            var studentLessonQuery = _studentRepo.Query<StudentLesson>();

            var query = from s in studentQuery
                        join c in classQuery
                        on s.ClassId equals c.Id into studentclasses
                        from studentcategory in studentclasses.DefaultIfEmpty()
                        join sl in studentLessonQuery 
                        on s.Id equals sl.StudentId into studentstudentlessons
                        from studentstudentlesson in studentstudentlessons.DefaultIfEmpty()
                        join l in lessonQuery
                        on studentstudentlesson.LessonId equals l.Id into lessonstudentlessons
                        from lessonsstudentlesson in lessonstudentlessons.DefaultIfEmpty()
                        select new ReportModel()
                        {
                            ClassName = studentcategory.Name,
                            //DateOfbirthDay = student.DateOfBirthday.HasValue ? student.DateOfBirthday.Value.ToString("MM/dd/yyyy") : "",
                            StudentName = s.Name,
                            schoolNo = s.SchoolNo.ToString(),
                            StudentSurName = s.SurName,
                            LessonName = lessonsstudentlesson.Name,
                            Numerical = lessonsstudentlesson.IsNumeric ? "Yes" : "No",



                            ClassId = studentcategory.Id,
                            LessonIds= lessonsstudentlesson.Id


                        };
            query = query.OrderBy(q => q.ClassName).ThenBy(q => q.StudentName);
            if (filter is not null)
            {
                if (!string.IsNullOrWhiteSpace(filter.StudentName))
                {
                    query = query.Where(q => q.StudentName.ToUpper().Contains(filter.StudentName.ToUpper().Trim()));
                }
                if (filter.ClassId.HasValue)
                {
                    query = query.Where(q => q.ClassId == filter.ClassId.Value);
                }
               
                if (!string.IsNullOrWhiteSpace(filter.StudentSurName))
                {
                    query = query.Where(q => q.StudentSurName.ToUpper().Contains(filter.StudentSurName.ToUpper().Trim()));
                }
                if (filter.LessonIds != null && filter.LessonIds.Count > 0)
                {
                    query = query.Where(q => filter.LessonIds.Contains(q.LessonIds ?? 0));
                }
            }
            return query.ToList();
        }


    }
}
