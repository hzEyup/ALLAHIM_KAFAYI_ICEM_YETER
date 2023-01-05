#nullable disable
using AppCore.Records.Bases;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class StudentModel : RecordBase
    {
        [Required(ErrorMessage = "{0} Cannot be empty!")]
        [StringLength(30, ErrorMessage = "{0} must be have max{1} characters!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} cannot be empty!")]
        [StringLength(50, ErrorMessage = "{0} must be have max {1} charcters! ")]
        [DisplayName("Sur Name")]
        public string SurName { get; set; }

        [Required(ErrorMessage = "{0} Cannot be empty!")]
        [StringLength(4, ErrorMessage = "{0} must be have {1} characters!")]
        [Range(0, 1000, ErrorMessage = "{0} must be only between {1} and {2}")]
        [DisplayName("School No")]
        public string SchoolNo { get; set; }

        public DateTime? DateOfBirthday { get; set; }

        [Required]
        [DisplayName("Class")]
        public int? ClassId { get; set; }


        [DisplayName("Class")]
        public string ClassNameDisplay { get; set; }

        //[DisplayName("School No")]
        //public string SchoolNoDisplay { get; set; }

        [DisplayName("Date Of Birthday")]
        public string DateOfBirthdayDisplay { get; set; }

        [DisplayName("Lessons")]
        public string LessonDisplay { get; set; }

        [DisplayName("Lessons")] 
        public List<int> LessonIds { get; set; }
    }
}
