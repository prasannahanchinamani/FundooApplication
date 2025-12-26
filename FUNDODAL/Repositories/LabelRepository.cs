using DataAccessLayer.CONTEXT;
using DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using ModelLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class LabelRepository : ILabelRepository
    {
        private readonly FunDooDBContext context;

        public LabelRepository(FunDooDBContext context)
        {
            this.context = context;
        }

        public void AddLabelToNote(int noteId, int labelId)
        {
            var exists = context.NoteLabels
                 .Any(nl => nl.NotesId == noteId && nl.LabelId == labelId);

            if (!exists)
            {
                context.NoteLabels.Add(new NoteLabel
                {
                    NotesId = noteId,
                    LabelId = labelId
                });
                context.SaveChanges();
            }

        }

        public Label CreateLabel(Label label)
        {
            context.Labels.Add(label);
            context.SaveChanges();
            return label;
        }

        public void DeleteLabel(int labelId, int userId)
        {
            var label = context.Labels
               .FirstOrDefault(l => l.LabelId == labelId && l.UserId == userId);
            if (label != null)
            {
                context.Labels.Remove(label);
                context.SaveChanges();
            }
            if (label == null) return;

        }
        public List<Label> GetAllLabelsByUser(int userId)
        {
            return context.Labels
                 .Where(l => l.UserId == userId)
                 .ToList();

        }

        public void RemoveLabelFromNote(int noteId, int labelId)
        {
            var link = context.NoteLabels
               .FirstOrDefault(nl => nl.NotesId == noteId && nl.LabelId == labelId);

            if (link == null) return;

           context.NoteLabels.Remove(link);
            context.SaveChanges();
        }

    }
  }
