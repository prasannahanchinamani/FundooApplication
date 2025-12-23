using DataAccessLayer.CONTEXT;
using DataAccessLayer.Interfaces;
using ModelLayer.Enities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly FunDooDBContext context;

        public UserRepository(FunDooDBContext context)
        {
            this.context = context;
        }
        public User AddUser(User user)
        {
            context.Users.Add(user);
            context.SaveChanges();
            return user;
        }
        public void DeleteUser(int id)
        {
            var findUser = context.Users.Find(id);
            if (findUser != null)
            {
                context.Users.Remove(findUser);
                context.SaveChanges();
            }
        }
        public List<User> GetAllUser()
        {
            return context.Users.ToList();
        }

        public User GetUserByEmail(string email)
        {
            return context.Users.FirstOrDefault(u => u.Email == email);
        }

        public User GetUserById(int id)
        {
            return context.Users.Find(id);
        }

        public User UpdateUser(User user)
        {
            context.Users.Update(user);
            context.SaveChanges();
            return user;
        }
    }

}
