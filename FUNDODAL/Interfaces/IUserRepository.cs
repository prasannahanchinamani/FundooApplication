
using ModelLayer.Enities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IUserRepository
    {
        User AddUser(User user);
        User GetUserById(int id);
        User GetUserByEmail(string email);
        List<User> GetAllUser();
        User UpdateUser(User user);
        void DeleteUser(int id);
        //User RegisteredUser(RegisterUserRequestDTO requestDTO);


    }
}
