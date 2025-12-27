using ModelLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface ICollaboratorRepository
    {
        NoteCollaborator Add(string email, int noteId, int userId);
        bool Remove(string email, int noteId, int userId);
        string UserNameById(int userId);
      }
}
