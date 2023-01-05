#nullable disable
using AppCore.Records.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models
{
    public class ClassModel : RecordBase
    {
        [Required(ErrorMessage = "{0} Cannot be empty!")]
        [StringLength(4)]
        public string Name { get; set; }


        ////
        /// <summary>
        /// 
        /// </summary>
        /// 
        [DisplayName("Student Count")]
		public int StudentCountDisplay { get; set; }

    }
}
