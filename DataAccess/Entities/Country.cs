#nullable disable
using AppCore.Records.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class Country : RecordBase
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public ICollection<City> Cities { get; set; }

        public ICollection<UserDetail> UserDetails { get; set; }
    }
}
