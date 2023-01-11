using Business.Models;
using DataAccess.Entities;
using DataAccess.Repositories;

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
            //var lessonQuery = _studentRepo.Query<Lesson>();
            var studentLessonQuery = _studentRepo.Query<StudentLesson>();
            var query = from student in studentQuery
                        join Class in classQuery
                        on student.ClassId equals Class.Id
                        //join studentLesson in studentLessonQuery
                        //on student.Id equals studentLesson.StudentId
                        //join lesson in lessonQuery
                        //on student/*Lesson.LessonId equals lesson.Id*/
                       
                        
                       
                        
                        select new ReportModel()
                        {
                           ClassName = Class.Name,
                           DateOfbirthDay = student.DateOfBirthday.HasValue ? student.DateOfBirthday.Value.ToString("MM/dd/yyyy") : "",
                           StudentName = student.Name,
                           schoolNo = student.SchoolNo.ToString(),
                           StudentSurName = student.SurName,
                           //LessonName = lesson.Name,
                           //Numerical = lesson.IsNumeric ? "Yes" : "No",
                           


                           ClassId = Class.Id,

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
            }
            return query.ToList();
        }

        
    }
}
