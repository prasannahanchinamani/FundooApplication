using DataAccessLayer.CONTEXT;
using DataAccessLayer.Interfaces;
using ModelLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLayer.Repositories
{
    public class NotesRepository : INotesRepository
    {
        private readonly FunDooDBContext context;

        public NotesRepository(FunDooDBContext context)
        {
            this.context = context;
        }

        public Notes CreateNote(Notes notes, int userId)
        {
            notes.UserId = userId;
            notes.CreatedAt = DateTime.UtcNow;
            notes.UpdatedAt = DateTime.UtcNow;
            context.Notes.Add(notes);
            context.SaveChanges();
            return notes;
        }

        public List<Notes> GetAllNotesByUser(int userId)
        {
            return context.Notes
                .Where(n => n.UserId == userId && !n.IsTrash)
                .ToList();
        }

        public Notes GetNoteById(int noteId, int userId)
        {
            return context.Notes
                .FirstOrDefault(n => n.NotesId == noteId && n.UserId == userId);
        }

        public Notes UpdateNote(Notes notes, int userId)
        {
            notes.UpdatedAt = DateTime.UtcNow;
            context.Notes.Update(notes);
            context.SaveChanges();
            return notes;
        }

        //  Move to Trash (SOFT DELETE)
        public void MoveToTrash(int noteId, int userId)
        {
            var note = context.Notes.FirstOrDefault(n =>
                n.NotesId == noteId &&
                n.UserId == userId &&
                !n.IsTrash);

            if (note != null)
            {
                note.IsTrash = true;
                context.SaveChanges();
            }
        }

        //  Get Trashed Notes
        public List<Notes> GetTrashedNotes(int userId)
        {
            return context.Notes
                .Where(n => n.UserId == userId && n.IsTrash)
                .ToList();
        }

        //  Restore from Trash
        public void RestoreNote(int noteId, int userId)
        {
            var note = context.Notes.FirstOrDefault(n =>
                n.NotesId == noteId &&
                n.UserId == userId &&
                n.IsTrash);

            if (note != null)
            {
                note.IsTrash = false;
                context.SaveChanges();
            }
        }

        //  Permanent Delete (ONLY FROM TRASH)
        public void PermanentDelete(int noteId, int userId)
        {
            var note = context.Notes.FirstOrDefault(n =>
                n.NotesId == noteId &&
                n.UserId == userId &&
                n.IsTrash);

            if (note != null)
            {
                context.Notes.Remove(note);
                context.SaveChanges();
            }
        }

        public void ArchiveNote(int noteId, int userId)
        {
            var note = GetNoteById(noteId, userId);
            if (note != null)
            {
                note.IsArchive = true;
                context.SaveChanges();
            }
        }

        public void UnarchiveNote(int noteId, int userId)
        {
            var note = GetNoteById(noteId, userId);
            if (note != null)
            {
                note.IsArchive = false;
                context.SaveChanges();
            }
        }

        public void PinNote(int noteId, int userId)
        {
            var note = GetNoteById(noteId, userId);
            if (note != null)
            {
                note.IsPin = true;
                context.SaveChanges();
            }
        }

        public void UnpinNote(int noteId, int userId)
        {
            var note = GetNoteById(noteId, userId);
            if (note != null)
            {
                note.IsPin = false;
                context.SaveChanges();
            }
        }

        public void ChangeColor(int noteId, int userId, string colour)
        {
            var note = GetNoteById(noteId, userId);
            if (note != null)
            {
                note.Colour = colour;
                note.UpdatedAt = DateTime.UtcNow;
                context.SaveChanges();
            }
        }
    }
}
