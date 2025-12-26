using ModelLayer.Entities;
using System.Collections.Generic;

namespace DataAccessLayer.Interfaces
{
    public interface ILabelRepository
    {
        Label CreateLabel(Label label);
        List<Label> GetAllLabelsByUser(int userId);
        void DeleteLabel(int labelId, int userId);

        void AddLabelToNote(int noteId, int labelId);
        void RemoveLabelFromNote(int noteId, int labelId);
    }
}
