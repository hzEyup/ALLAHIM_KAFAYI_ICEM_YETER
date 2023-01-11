#nullable disable

using System.ComponentModel;

namespace Business.Models
{
    public class ReportFilterModel
    {
        [DisplayName("Student Name")]
        public string StudentName { get; set; }

        [DisplayName("---- All Class ----")]
        public int? ClassId { get; set; }

        [DisplayName("Student Sur Name")]
        public string StudentSurName { get; set; }

    }
}
