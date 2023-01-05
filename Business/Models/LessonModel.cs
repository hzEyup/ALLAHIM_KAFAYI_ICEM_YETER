#nullable disable

using AppCore.Records.Bases;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Business.Models
{
    public class LessonModel : RecordBase
    {
        #region Entity'den Kopyalanan Özellikler
        [Required(ErrorMessage = "{0} Cannot be empty!")]
        [StringLength(150, ErrorMessage = "{0} must be maximum {1} characters!")]
        [DisplayName("Lesson Name")]
        public string Name { get; set; }

        [DisplayName("Numerical ")]
        public bool IsNumeric { get; set; }
        #endregion



        #region View'larda Gösterim için Kullanacağımız Özellikler
        [DisplayName("Numerical ")]
        public string NumericalDisplay { get; set; }
        #endregion
    }
}
