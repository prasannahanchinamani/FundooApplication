using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.DTO
{
    public class RegisterUserRequestDTO
    {
        //public int UserId { set; get; }
        [Required(ErrorMessage = "Must be Greater than 2 Character")]
        [MinLength(2)]
        [MaxLength(100)]
        public string FirstName { set; get; }

        [Required(ErrorMessage = "Must be Greater than 1 Character")]
        [MinLength(1)]
        [MaxLength(100)]
        public string LastName { set; get; }

        [EmailAddress(ErrorMessage = "Email format is Not valid")]
        [MaxLength(100)]
        public string Email { set; get; }

        [Required(ErrorMessage = "PassWord Must be 6 Character or greater  Than 6")]
        [MinLength(6)]
        public string Password { set; get; }


    }
}
