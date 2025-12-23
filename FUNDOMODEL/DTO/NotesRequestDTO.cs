using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.DTO
{
public class NotesRequestDTO
    {
        [Required(ErrorMessage = "Must be Greater than 3 Character")]
        [MinLength(3)]
        [MaxLength(250)]
        public string Title { get; set; }
        [Required]
        [MaxLength(250)]
        public string Description { get; set; }
       
        public DateTime? Reminder { get; set; }
        public string? Colour { get; set; }

    }
}
