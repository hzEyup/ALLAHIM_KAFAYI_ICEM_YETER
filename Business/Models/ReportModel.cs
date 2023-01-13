#nullable disable

using System.ComponentModel;

namespace Business.Models
{
    public class ReportModel
    {
        #region 


        [DisplayName("Student Name")]
        public string StudentName { get; set; }

        [DisplayName("School No")]
        public string schoolNo { get; set; }


        [DisplayName("Date Of Birth Day")]
        public string DateOfbirthDay { get; set; }

        [DisplayName("Class")]
        public string ClassName { get; set; }

        [DisplayName("Lesson")]
        public string LessonName { get; set; }

        [DisplayName("Numerical")]
        public string Numerical { get; set; }

        [DisplayName("Sur Name")]
        public string StudentSurName { get; set; }

 

        #endregion

        #region Filter
        public int ClassId { get; set; }
        public int? LessonIds { get; set; }
        #endregion
    }
}
