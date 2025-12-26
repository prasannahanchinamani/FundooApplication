using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Services;
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

      
        [HttpPost]
        public IActionResult CreateLabel([FromBody] LabelRequestDTO dto)
        {
            var result = _labelService.CreateLabel(dto, GetUserId());
            return Ok(result);
        }

    
        [HttpGet]
        public IActionResult GetLabels([FromQuery] string? search)
        {
           
            var result = _labelService.GetLabels(GetUserId());
            return Ok(result);
        }

   
        [HttpDelete("{labelId}")]
        public IActionResult DeleteLabel([FromRoute] int labelId)
        {
            _labelService.DeleteLabel(labelId, GetUserId());
            return Ok(new { Message = "Label deleted successfully" });
        }

        [HttpPost("notes/{noteId}/labels/{labelId}")]
        public IActionResult AddLabelToNote(
            [FromRoute] int noteId,
            [FromRoute] int labelId)
        {
            _labelService.AddLabelToNote(noteId, labelId);
            return Ok(new { Message = "Label added to note" });
        }

        [HttpDelete("notes/{noteId}/labels/{labelId}")]
        public IActionResult RemoveLabelFromNote(
            [FromRoute] int noteId,
            [FromRoute] int labelId)
        {
            _labelService.RemoveLabelFromNote(noteId, labelId);
            return Ok(new { Message = "Label removed from note" });
        }
    }
}
