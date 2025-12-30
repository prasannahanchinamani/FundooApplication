using BusinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FunDooApplication.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/reminder")]
    public class ReminderController : ControllerBase
    {
        private readonly IReminderService reminderService;

        public ReminderController(IReminderService reminderService)
        {
            this.reminderService = reminderService;
        }

        
        [Authorize]
        [HttpPost("send")]
        public IActionResult SendReminders()
        {
            reminderService.sendRemindersToEmail();
            return Ok("Reminder emails sent successfully");
        }
    }
}
