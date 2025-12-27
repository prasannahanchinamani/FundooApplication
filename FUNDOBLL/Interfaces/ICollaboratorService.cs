using ModelLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface ICollaboratorService
    {
        CollaboratorResponseDTO AddCollaborator(string email, int noteId, int userId);
        bool RemoveCollaborator(string email, int noteId, int userId);
    }
}
