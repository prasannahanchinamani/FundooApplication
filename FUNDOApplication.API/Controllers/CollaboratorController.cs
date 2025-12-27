using BusinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FunDooApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CollaboratorController : ControllerBase
    {
        private readonly ICollaboratorService service;

        public CollaboratorController(ICollaboratorService service)
        {
            this.service = service;
        }
        [HttpPost("add")]
        public IActionResult Add(string email, int noteId)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var result = service.AddCollaborator(email, noteId, userId);

            if (result == null)
                return BadRequest("Collaborator already exists");

            return Ok(result);
        }
        [HttpDelete("remove")]
        public IActionResult Remove(string email, int noteId)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var success = service.RemoveCollaborator(email, noteId, userId);

            if (!success)
                return NotFound("Collaborator not found");

            return Ok("Collaborator removed successfully");
        }

    }
}
