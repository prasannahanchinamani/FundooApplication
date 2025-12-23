using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.DTO
{
   public class LoginRequestDTO
    {
        [EmailAddress(ErrorMessage = "Email format is Not valid")]
        [MaxLength(100)]
        public string Email { set; get; }

        [Required(ErrorMessage = "PassWord Must be 6 Character or greater  Than 6")]
        [MinLength(6)]
        public string Password { set; get; }

    }
}
