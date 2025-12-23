using AutoMapper;
using ModelLayer.AutoMapper;
using ModelLayer.DTO;
using ModelLayer.Entities;

namespace ModelLayer.AutoMapper
{
    public class NotesProfile : Profile
    {
        public NotesProfile()
        {
            CreateMap<NotesRequestDTO, Notes>();   
            CreateMap<Notes, NotesResponseDTO>();  
        }
    }
}
