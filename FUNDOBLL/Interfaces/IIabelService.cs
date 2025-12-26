using ModelLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IlabelService
    {
        LabelResponseDTO CreateLabel(LabelRequestDTO dto, int userId);
        IEnumerable<LabelResponseDTO> GetLabels(int userId);
        void DeleteLabel(int labelId, int userId);

        void AddLabelToNote(int noteId, int labelId);
        void RemoveLabelFromNote(int noteId, int labelId);
    }
}
