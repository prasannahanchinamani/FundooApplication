using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Entities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { set; get; }

        public string FirstName { set; get; }

        public string LastName { set; get; }


        public string Email { set; get; }
        //public string? Colour { get; set; }

        public string Password { set; get; }

        public DateTime CreatedAt { get; set; }
        public DateTime ChangedAt { get; set; }
    }
}
