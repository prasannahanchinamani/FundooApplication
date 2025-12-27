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
  public class CollaboratorService: ICollaboratorService
    {
        private readonly ICollaboratorRepository repository;
        private readonly IMapper mapper;
        private readonly IEmailService emailService;
       

        public CollaboratorService(ICollaboratorRepository repository,IMapper mapper,IEmailService emailService)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.emailService = emailService;   
        }
        public CollaboratorResponseDTO AddCollaborator(string email, int noteId, int userId)
        {
            var collaborator = repository.Add(email, noteId, userId);

            if (collaborator == null)
                return null;

            string userName = repository.UserNameById(userId) ?? "Someone";

            string subject = "You're invited to collaborate";

            string body = $"Hi,\n\n" +
                          $"{userName} has added you as a collaborator on a note.\n\n" +
                          $"You can now view and work on the note together.\n\n" +
                          $"Thank you,\n" +
                          $"FunDoo Team";

            emailService.Send(email, subject, body);

            return mapper.Map<CollaboratorResponseDTO>(collaborator);
        }


        public bool RemoveCollaborator(string email, int noteId, int userId)
        {

            return repository.Remove(email, noteId, userId);

        }
    }
}
