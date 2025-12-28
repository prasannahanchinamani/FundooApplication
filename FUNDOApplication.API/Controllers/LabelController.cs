using BusinessLogicLayer.Exceptions.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.DTO;
using System.Security.Claims;

namespace FundooApp.Controllers
{
    [ApiController]
    [Route("api/labels")]
    [Authorize]
    public class LabelController : ControllerBase
    {
        private readonly IlabelService _labelService;

        public LabelController(IlabelService labelService)
        {
            _labelService = labelService;
        }

        private int GetUserId()
        {
            return int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        }

        // POST /api/labels
        [HttpPost]
        public IActionResult CreateLabel([FromBody] LabelRequestDTO dto)
        {
            return Ok(_labelService.CreateLabel(dto, GetUserId()));
        }

        // GET /api/labels
        [HttpGet]
        public IActionResult GetLabels()
        {
            return Ok(_labelService.GetLabels(GetUserId()));
        }

        // PUT /api/labels/{labelId}
        [HttpPut("{labelId}")]
        public IActionResult UpdateLabelName(int labelId, [FromBody] string name)
        {
            var result = _labelService.UpdateLabel(labelId, name);

            if (result == null)
                return NotFound("Label not found");

            return Ok(result);
        }

        // DELETE /api/labels/{labelId}
        [HttpDelete("{labelId}")]
        public IActionResult DeleteLabel(int labelId)
        {
            _labelService.DeleteLabel(labelId, GetUserId());
            return Ok("Label deleted successfully");
        }

        // POST /api/labels/{labelId}/notes/{noteId}
        [HttpPost("{labelId}/notes/{noteId}")]
        public IActionResult AddLabelToNote(int labelId, int noteId)
        {
            _labelService.AddLabelToNote(noteId, labelId);
            return Ok("Label added to note");
        }

        // DELETE /api/labels/{labelId}/notes/{noteId}
        [HttpDelete("{labelId}/notes/{noteId}")]
        public IActionResult RemoveLabelFromNote(int labelId, int noteId)
        {
            _labelService.RemoveLabelFromNote(noteId, labelId);
            return Ok("Label removed from note");
        }

        // GET /api/labels/{labelId}/notes
        [HttpGet("{labelId}/notes")]
        public IActionResult GetNotesByLabel(int labelId)
        {
            return Ok(_labelService.GetLabelByLabelId(labelId));
        }

    }
}
