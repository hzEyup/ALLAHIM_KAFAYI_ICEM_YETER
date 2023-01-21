#nullable disable
using AppCore.Records.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class Student:RecordBase
    {
        [Required(ErrorMessage ="{0} Cannot be empty!")]
        [StringLength(30, ErrorMessage ="{0} must be have max{1} characters!")]
        public string Name { get; set; }

        [Required(ErrorMessage ="{0} cannot be empty!")]
        [StringLength(50, ErrorMessage ="{0} must be have max {1} charcters! ")]
        public string SurName { get; set; }

        [Required(ErrorMessage = "{0} Cannot be empty!")]
        [StringLength(4, ErrorMessage ="{0} must be have {1} characters!")]
        public int SchoolNo { get; set; }

        public DateTime? DateOfBirthday { get; set; }
        public int ClassId { get; set; }

        [MaxLength(5)]
        public string ImgExtension { get; set; }

        [Column(TypeName ="image")]
        public byte[] Image { get; set; }
        public Class Class { get; set; }
        public List<StudentLesson> studentLessons { get; set; }
    }
}
