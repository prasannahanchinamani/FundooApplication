using BusinessLogicLayer.Exceptions.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FunDooApplication.Controllers
{
    [ApiController]
    [Route("api/collaborators")]
    [Authorize]
    public class CollaboratorController : ControllerBase
    {
        private readonly ICollaboratorService service;

        public CollaboratorController(ICollaboratorService service)
        {
            this.service = service;
        }

        private int GetUserId()
        {
            return int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        }

        [HttpPost]
        public IActionResult AddCollaborator([FromQuery] string email, [FromQuery] int noteId)
        {
            var result = service.AddCollaborator(email, noteId, GetUserId());
            if (result == null) return BadRequest("Collaborator already exists");
            return Ok(result);
        }

        [HttpGet("{noteId}")]
        public IActionResult GetCollaboratorsByNote(int noteId)
        {
            return Ok(service.GetCollaboratorsByNoteId(noteId));
        }

        [HttpDelete("{collaboratorId:int}")]
        public IActionResult RemoveCollaboratorById(int collaboratorId)
        {
            var result = service.RemoveCollaboratorById(collaboratorId);
            if (result == null) return NotFound("Collaborator not found");
            return Ok(result);
        }

        [HttpDelete("{noteId}/{email}")]
        public IActionResult RemoveCollaboratorByEmail(int noteId, string email)
        {
            var success = service.RemoveCollaborator(email, noteId, GetUserId());
            if (!success) return NotFound("Collaborator not found");
            return Ok("Collaborator removed successfully");
        }

        [HttpGet("shared-notes")]
        public IActionResult GetSharedNotes()
        {
            return Ok(service.GetSharedNotes(GetUserId()));
        }
    }
}
