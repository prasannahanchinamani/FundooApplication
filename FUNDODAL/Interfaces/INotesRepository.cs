using ModelLayer.Entities;
using System.Collections.Generic;

namespace DataAccessLayer.Interfaces
{
    public interface INotesRepository
    {
        Notes CreateNote(Notes notes, int userId);

        List<Notes> GetAllNotesByUser(int userId);

        Notes GetNoteById(int noteId, int userId);

        Notes UpdateNote(Notes notes, int userId);

        void MoveToTrash(int noteId, int userId);

        List<Notes> GetTrashedNotes(int userId);

        void RestoreNote(int noteId, int userId);

        void PermanentDelete(int noteId, int userId);

        void ArchiveNote(int noteId, int userId);

        void UnarchiveNote(int noteId, int userId);

        void PinNote(int noteId, int userId);

        void UnpinNote(int noteId, int userId);

        //void ChangeColor(int noteId, string color, int userId);
        void ChangeColor(int noteId, int userId, string colour);




    }
}
