#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class StudentLesson
    {
        [Key]
        [Column(Order = 0)]
        public int StudentId { get; set; }

        public Student Student { get; set; }

        [Key]
        [Column(Order = 1)]
        public int LessonId { get; set; }

        public Lesson Lesson { get; set; }
    }
}
