#nullable disable
using AppCore.Records.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class Lesson : RecordBase
    {
        [Required]
        [StringLength(50)]
        [DisplayName("Lesson Name")]
        public string Name { get; set; }

        [Required]
        [DisplayName("numerical ?")]
        public bool IsNumeric { get; set; }
        public bool IsDeleted { get; set; }
        public List<StudentLesson> StudentLessons { get; set; }
    }
}
