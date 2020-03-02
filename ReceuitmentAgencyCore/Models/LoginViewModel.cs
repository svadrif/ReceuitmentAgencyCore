using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RecruitmentAgencyCore.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Foydalanuvchi nomi bo'sh bulishi mumkin emas")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Parol bo'sh bulishi mumkin emas")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }
    }
}
