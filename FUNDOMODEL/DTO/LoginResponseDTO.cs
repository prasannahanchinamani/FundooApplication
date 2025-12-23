using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.DTO
{
    public class LoginResponseDTO
    {
        public int UserId { set; get; }
       
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public string Email { set; get; }

    }
}
