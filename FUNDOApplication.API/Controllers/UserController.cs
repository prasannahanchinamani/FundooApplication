using BusinessLogicLayer.Exceptions.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.DTO;

namespace FunDooApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly ILogger<UserController> logger;   

        public UserController(
            IUserService userService,
            ILogger<UserController> logger)               
        {
            this.userService = userService;
            this.logger = logger;                         
        }


        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody]RegisterUserRequestDTO dto)
        {
            logger.LogInformation("User Register API called");
            var result = userService.RegisteredUser(dto);

            return Ok(new
            {
                Message = "User registered successfully",
                User = result
            });
        }


        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody]LoginRequestDTO dto)
        {
            logger.LogInformation("User Login API called");
            var user = userService.LogedInUser(dto);


            var token = userService.GenerateLoginJwt(user);

            return Ok(new
            {
                Message = "Login successful",
                Token = token,
                UserId = user.UserId,
                Email = user.Email
            });
        }


        //[AllowAnonymous]
        [AllowAnonymous]
        [HttpPost("forgot-password")]
        public IActionResult ForgotPassword([FromQuery] string email)
        {
            logger.LogInformation("Forgot Password API called");
            userService.ForgotPassword(email);
            return Ok($"reset token sent to {email}");
        }



        [AllowAnonymous]
        [HttpPost("reset-password")]
        public IActionResult ResetPassword(string token, string newPassword)
        {
            logger.LogInformation("Reset Password API called");
            userService.ResetPassword(token, newPassword);
            return Ok("Password reset successful");
        }

       
        [Authorize]
        [HttpGet("users")]
        public IActionResult GetAllUsers()
        {
            logger.LogInformation("Get All Users API called");
            return Ok(userService.GetAllUser());
        }
    }
}
