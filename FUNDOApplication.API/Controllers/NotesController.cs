using BusinessLogicLayer.Exceptions.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.DTO;
using System.Security.Claims;

namespace FunDooApplication.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/notes")]
    public class NotesController : ControllerBase
    {
        private readonly INoteServices services;

        public NotesController(INoteServices services)
        {
            this.services = services;
        }

        private int GetUserId()
        {
            return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }

        [HttpPost]
        public IActionResult CreateNote(NotesRequestDTO dto)
            => Ok(services.CreateNotes(dto, GetUserId()));

        [HttpGet]
        public IActionResult GetAllNotes()
            => Ok(services.GetAllNotes(GetUserId()));

        [HttpGet("{noteId}")]
        public IActionResult GetNoteById(int noteId)
            => Ok(services.GetNoteById(noteId, GetUserId()));

        [HttpPut("{noteId}")]
        public IActionResult UpdateNote(int noteId, NotesRequestDTO dto)
            => Ok(services.UpdateNote(noteId, dto, GetUserId()));

        [HttpDelete("{noteId}")]
        public IActionResult MoveToTrash(int noteId)
        {
            services.MoveToTrash(noteId, GetUserId());
            return Ok("Note moved to trash");
        }

        [HttpGet("trash")]
        public IActionResult GetTrashedNotes()
            => Ok(services.GetAllTrashedNotes(GetUserId()));

        [HttpPatch("{noteId}/restore")]
        public IActionResult Restore(int noteId)
        {
            services.RestoreNote(noteId, GetUserId());
            return Ok("Note restored");
        }

        [HttpDelete("{noteId}/permanent")]
        public IActionResult PermanentDelete(int noteId)
        {
            services.PermanentDelete(noteId, GetUserId());
            return Ok("Note permanently deleted");
        }

        [HttpGet("archive")]
        public IActionResult GetArchivedNotes()
            => Ok(services.GetArchivedNotes(GetUserId()));

        [HttpPatch("{noteId}/archive")]
        public IActionResult Archive(int noteId)
        {
            services.ArchiveNote(noteId, GetUserId());
            return Ok("Note archived");
        }

        [HttpPatch("{noteId}/unarchive")]
        public IActionResult Unarchive(int noteId)
        {
            services.UnarchiveNote(noteId, GetUserId());
            return Ok("Note unarchived");
        }

        [HttpPatch("{noteId}/pin")]
        public IActionResult Pin(int noteId)
        {
            services.PinNote(noteId, GetUserId());
            return Ok("Note pinned");
        }

        [HttpPatch("{noteId}/unpin")]
        public IActionResult Unpin(int noteId)
        {
            services.UnpinNote(noteId, GetUserId());
            return Ok("Note unpinned");
        }

        [HttpPatch("{noteId}/color")]
        public IActionResult ChangeColor(int noteId, string colour)
        {
            services.ChangeColor(noteId, colour, GetUserId());
            return Ok("Color updated");
        }
    }
}
