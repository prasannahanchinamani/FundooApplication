using BusinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ModelLayer.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FunDooApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IConfiguration configuration;

        public UserController(IUserService userService, IConfiguration configuration)
        {
            this.userService = userService;
            this.configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register(RegisterUserRequestDTO dto)
        {
            var result = userService.RegisteredUser(dto);

            return Ok(new
            {
                Message = "User registered successfully",
                User = result
            });
        }


        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login(LoginRequestDTO dto)
        {
            var user = userService.LogedInUser(dto);

            if (user == null || user.UserId <= 0)
            {
                return Unauthorized(new { Message = "Invalid email or password" });
            }

         
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.FirstName)
            };

        
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration["Jwt:Key"])
            );

          
            var token = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials:
                    new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new
            {
                Message = "Login successful",
                Token = tokenString,
                UserId = user.UserId,
                Email = user.Email
            });
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = userService.GetAllUser();
            return Ok(users);
        }
    }
}
