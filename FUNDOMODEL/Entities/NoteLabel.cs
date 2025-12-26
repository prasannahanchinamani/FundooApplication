using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Entities
{
    // junction table to eastblish many to many relatioonship 
    // label to notes 

    public  class NoteLabel
    {
        public int NotesId { get; set; }   
        public int LabelId { get; set; }   
    }
}

