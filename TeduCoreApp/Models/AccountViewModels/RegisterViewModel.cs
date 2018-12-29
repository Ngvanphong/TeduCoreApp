using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TeduCoreApp.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Xác nhận mật khẩu")]
        [Compare("Password", ErrorMessage = "Confirm Password isn't corrected.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Tên đăng nhập")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Số điện thoại")]
        public string PhoneNumber { get; set; }
       
        [Display(Name = "Họ và tên")]
        public string FullName { get; set; }
    }
}
