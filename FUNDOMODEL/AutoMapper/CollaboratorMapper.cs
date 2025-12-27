using AutoMapper;
using ModelLayer.DTO;
using ModelLayer.Entities;

namespace ModelLayer.AutoMapper
{
    public class CollaboratorMapper : Profile
    {
        public CollaboratorMapper()
        {
            CreateMap<NoteCollaborator, CollaboratorResponseDTO>();
        }
    }
}
