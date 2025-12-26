using BusinessLogicLayer.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer.DTO;
using DataAccessLayer.Interfaces;
using ModelLayer.Entities;
using Microsoft.Identity.Client;
using BCrypt.Net;
using BusinessLogicLayer.Exceptions;



namespace BusinessLogicLayer.Services
{
    public class UserServiceRepo : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IEmailService emailService;
        public UserServiceRepo(IUserRepository userRepository, IEmailService emailService)
        {
            this.userRepository = userRepository;
            this.emailService = emailService;
        }
        public List<RegisterUserResponseDTO> GetAllUser()
        {
            var users = userRepository.GetAllUser();
            if (users == null || users.Count == 0)
            {
                throw new Exception("No user Founds");
            }
            return users.Select(u => new RegisterUserResponseDTO
            {
                UserId = u.UserId,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                CreatedAt = u.CreatedAt,
                UpdatedAt = u.ChangedAt
            }).ToList();
        }

        public LoginResponseDTO LogedInUser(LoginRequestDTO requestDto)
        {
            var user = userRepository.GetUserByEmail(requestDto.Email);


            if (user == null)
            {
                throw new UserNotFoundException("User is Not Registered");
            }

            bool isPasswordValid =
                BCrypt.Net.BCrypt.Verify(requestDto.Password, user.Password);


            if (!isPasswordValid)
            {
                throw new PasswordInvalidException("Password is Wrong");
            }

            return new LoginResponseDTO
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
            };

        }


        public RegisterUserResponseDTO RegisteredUser(RegisterUserRequestDTO requestDto)
        {

            var existingUser = userRepository.GetUserByEmail(requestDto.Email);
            if (existingUser != null)
            {
                throw new EmailNotFoundException("User is Already Register");
            }
            User user = new User
            {
                FirstName = requestDto.FirstName,
                LastName = requestDto.LastName,
                Email = requestDto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(requestDto.Password),
                CreatedAt = DateTime.UtcNow,
                ChangedAt = DateTime.UtcNow,
            };
            userRepository.AddUser(user);
            emailService.Send(
                       user.Email,
                   "Registration Successful ",
                    $"Hello {user.FirstName} , " +
                    $"your registration was successful." +
                    $"Thank You Use This App"
);



            return new RegisterUserResponseDTO
            {
                UserId = user.UserId,
                FirstName = requestDto.FirstName,
                LastName = requestDto.LastName,
                Email = requestDto.Email,
            };

        }
    }
}
