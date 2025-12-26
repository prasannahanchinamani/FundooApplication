
using ModelLayer.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer.DTO;

namespace BusinessLogicLayer.Interfaces
{
    public interface IUserService
    {
         RegisterUserResponseDTO RegisteredUser(RegisterUserRequestDTO requestDto);
         LoginResponseDTO   LogedInUser(LoginRequestDTO requestDto);
         List<RegisterUserResponseDTO> GetAllUser();
       
    }
}