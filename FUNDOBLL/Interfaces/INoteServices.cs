using ModelLayer.DTO;
using System.Collections.Generic;

namespace BusinessLogicLayer.Interfaces
{
    public interface INoteServices
    {
        NotesResponseDTO CreateNotes(NotesRequestDTO dto, int userId);

        List<NotesResponseDTO> GetAllNotes(int userId);

        NotesResponseDTO UpdateNote(int noteId, NotesRequestDTO dto, int userId);

        void MoveToTrash(int noteId, int userId);

        List<NotesResponseDTO> GetAllTrashedNotes(int userId);

        List<NotesResponseDTO> GetArchivedNotes(int userId);

        void ArchiveNote(int noteId, int userId);

        void UnarchiveNote(int noteId, int userId);

        void PinNote(int noteId, int userId);

        void UnpinNote(int noteId, int userId);

        void ChangeColor(int noteId, string colour, int userId);
        void RestoreNote(int noteId, int v);
        void PermanentDelete(int noteId, int v);
    }
}
