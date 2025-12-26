using AutoMapper;
using ModelLayer.DTO;
using ModelLayer.Entities;
//using ModelLayer.Entities;

namespace ModelLayer.AutoMapper
{
    public class LabelMapper : Profile
    {
        public LabelMapper()
        {
            CreateMap<LabelRequestDTO, Label>();
            CreateMap<Label, LabelResponseDTO>();
        }
    }
}
