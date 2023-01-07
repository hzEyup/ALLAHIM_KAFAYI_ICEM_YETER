#nullable disable

using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class AccountRegisterModel
    {
        [Required]
        [StringLength(25)]
        public string UserName { get; set; }

        [Required]
        [StringLength(10)]
        [Compare("ConfirmPassword", ErrorMessage = "Password and confirm password must be same!")]
        public string Password { get; set; }

        [Required]
        [StringLength(10)]
        public string ConfirmPassword { get; set; }
    }
}