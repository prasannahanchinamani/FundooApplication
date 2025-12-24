using AutoMapper;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using ModelLayer.DTO;
using ModelLayer.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

public class NotesService : INoteServices
{
    private readonly INotesRepository repo;
    private readonly IMapper mapper;
    private readonly IDistributedCache cache;

    public NotesService(INotesRepository repo, IMapper mapper, IDistributedCache cache)
    {
        this.repo = repo;
        this.mapper = mapper;
        this.cache = cache;
    }

    public NotesResponseDTO CreateNotes(NotesRequestDTO dto, int userId)
    {
        var note = mapper.Map<Notes>(dto);
        var saved = repo.CreateNote(note, userId);
        return mapper.Map<NotesResponseDTO>(saved);
    }

    public List<NotesResponseDTO> GetAllNotes(int userId)
    {
        //create ck key
        string cacheKey = $"note{userId}";
        //cache hit
        var cachedData=cache.GetString(cacheKey);
        
        if (cachedData != null)
        {
            //Console.WriteLine(JsonSerializer.Deserialize<List<NotesResponseDTO>>(cachedData));
            return JsonSerializer.Deserialize<List<NotesResponseDTO>>(cachedData);
        }
        //cache miss 
        //var notesFromDb=repo.GetAllNotesByUser(userId);
         var notesDto=mapper.Map<List<NotesResponseDTO>>(repo.GetAllNotesByUser(userId));
         
        var jsonData=JsonSerializer.Serialize(notesDto);
        cache.SetString(
            cacheKey,
              jsonData,
      new DistributedCacheEntryOptions
      {
          //Deletes the cache entry exactly 10 minutes after it is created, no matter how many times it is accessed
          AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
      });
        return notesDto;
    }

    public NotesResponseDTO UpdateNote(int noteId, NotesRequestDTO dto, int userId)
    {
        var note = repo.GetNoteById(noteId, userId);
        if (note == null) return null;

        mapper.Map(dto, note);
        var updated = repo.UpdateNote(note, userId);
        cache.Remove($"note{userId}");
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
