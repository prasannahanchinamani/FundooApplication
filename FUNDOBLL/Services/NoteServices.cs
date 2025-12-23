using AutoMapper;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Interfaces;
using ModelLayer.DTO;
using ModelLayer.Entities;
using System.Collections.Generic;
using System.Linq;

public class NotesService : INoteServices
{
    private readonly INotesRepository repo;
    private readonly IMapper mapper;

    public NotesService(INotesRepository repo, IMapper mapper)
    {
        this.repo = repo;
        this.mapper = mapper;
    }

    public NotesResponseDTO CreateNotes(NotesRequestDTO dto, int userId)
    {
        var note = mapper.Map<Notes>(dto);
        var saved = repo.CreateNote(note, userId);
        return mapper.Map<NotesResponseDTO>(saved);
    }

    public List<NotesResponseDTO> GetAllNotes(int userId)
    {
        return mapper.Map<List<NotesResponseDTO>>(repo.GetAllNotesByUser(userId));
    }

    public NotesResponseDTO UpdateNote(int noteId, NotesRequestDTO dto, int userId)
    {
        var note = repo.GetNoteById(noteId, userId);
        if (note == null) return null;

        mapper.Map(dto, note);
        var updated = repo.UpdateNote(note, userId);
        return mapper.Map<NotesResponseDTO>(updated);
    }

    public void MoveToTrash(int noteId, int userId)
        => repo.MoveToTrash(noteId, userId);

    public List<NotesResponseDTO> GetAllTrashedNotes(int userId)
        => mapper.Map<List<NotesResponseDTO>>(repo.GetTrashedNotes(userId));

    public void RestoreNote(int noteId, int userId)
        => repo.RestoreNote(noteId, userId);

    public void PermanentDelete(int noteId, int userId)
        => repo.PermanentDelete(noteId, userId);

    public List<NotesResponseDTO> GetArchivedNotes(int userId)
    {
        var notes = repo.GetAllNotesByUser(userId)
                        .Where(n => n.IsArchive)
                        .ToList();

        return mapper.Map<List<NotesResponseDTO>>(notes);
    }

    public void ArchiveNote(int noteId, int userId)
        => repo.ArchiveNote(noteId, userId);

    public void UnarchiveNote(int noteId, int userId)
        => repo.UnarchiveNote(noteId, userId);

    public void PinNote(int noteId, int userId)
        => repo.PinNote(noteId, userId);

    public void UnpinNote(int noteId, int userId)
        => repo.UnpinNote(noteId, userId);

    public void ChangeColor(int noteId, string colour, int userId)
        => repo.ChangeColor(noteId, userId, colour);
}
