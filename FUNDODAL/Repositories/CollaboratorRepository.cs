using DataAccessLayer.CONTEXT;
using DataAccessLayer.Interfaces;
using ModelLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class CollaboratorRepository : ICollaboratorRepository
    {
        private readonly FunDooDBContext context;

        public CollaboratorRepository(FunDooDBContext context)
        {
           this.context = context;
        }

        public NoteCollaborator Add(string email, int noteId, int userId)
        {
            var exists = context.NoteCollaborators
                .FirstOrDefault(x => x.Email == email && x.NoteId == noteId);

            if (exists != null)
                return null;

            var collaborator = new NoteCollaborator
            {
                Email = email,
                NoteId = noteId,
                UserId = userId
            };

            context.NoteCollaborators.Add(collaborator);
            context.SaveChanges();

            return collaborator;
        }

        public bool Remove(string email, int noteId, int userId)
        {
            var collaborator = context.NoteCollaborators
                .FirstOrDefault(x => x.Email == email
                                  && x.NoteId == noteId
                                  && x.UserId == userId);

            if (collaborator == null)
                return false;

            context.NoteCollaborators.Remove(collaborator);
            context.SaveChanges();

            return true;
        }
        public string UserNameById(int userId)
        {
            var user = context.Users.Find(userId);
            return user?.FirstName;
        }

    }
}
