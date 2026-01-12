using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.DTO
{
 public class RegisterUserResponseDTO
    {
        public int UserId { set; get; }
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public string Email { set; get; }
        // public string Password { set; get; }
        public DateTime CreatedAt {  set; get; }

        public DateTime UpdatedAt { set; get; }

    }
}
