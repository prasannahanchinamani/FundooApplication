using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.DTO
{
    public class CollaboratorResponseDTO
    {
        public int CollaboratorId { get; set; }

        public string Email { get; set; }

        //public int UserId { get; set; }

        public int NoteId { get; set; }
    }
}
