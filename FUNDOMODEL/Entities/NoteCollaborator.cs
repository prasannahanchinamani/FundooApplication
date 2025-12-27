using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Entities
{
  public class NoteCollaborator
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CollaboratorId { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

 
        [Required]
        public int UserId { get; set; }

      
        [Required]
        public int NoteId { get; set; }
    }
}
