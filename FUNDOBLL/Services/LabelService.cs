using AutoMapper;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Interfaces;
using ModelLayer.DTO;
using ModelLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class LabelService : IlabelService
        
    {
        private readonly ILabelRepository   labelRepository;
        private readonly IMapper mapper;

        public LabelService(ILabelRepository labelRepository, IMapper mapper)
        {
            this.labelRepository = labelRepository;
            this.mapper = mapper;
        }

        public void AddLabelToNote(int noteId, int labelId)
        {
            labelRepository.AddLabelToNote(noteId, labelId);
        }

        public LabelResponseDTO CreateLabel(LabelRequestDTO dto, int userId)
        {
            var label = new Label
            {
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                LabelName = dto.LabelName,
                UserId = userId
            };
            var requestDTO=labelRepository.CreateLabel(label);
            return mapper.Map<LabelResponseDTO>(requestDTO);

        }

        public void DeleteLabel(int labelId, int userId)
        {
           labelRepository.DeleteLabel(labelId, userId);
           
        }

        public IEnumerable<LabelResponseDTO> GetLabels(int userId)
        {
            var labesl= labelRepository.GetAllLabelsByUser(userId).ToList();

            return mapper.Map<IEnumerable<LabelResponseDTO>>(labesl);
            
        }

        public void RemoveLabelFromNote(int noteId, int labelId)
        {
           labelRepository.RemoveLabelFromNote(noteId, labelId);
        }
    }
}
