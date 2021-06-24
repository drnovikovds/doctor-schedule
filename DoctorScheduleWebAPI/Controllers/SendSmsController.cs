using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DoctorScheduleWebAPI.Services;

namespace DoctorScheduleWebAPI.Controllers
{
    [ApiController]
    public class SendSmsController : ControllerBase
    {
        private readonly SmsSenderService _smsSender;
        public SendSmsController (SmsSenderService smsSender)
        {
            _smsSender = smsSender;
        }

        [HttpGet("api/smssender")]

        public async Task<IActionResult> SendSmsAsync([FromQuery] string number, string message)
        {
            await _smsSender.SendSmsAsync(number, message);
            return new OkResult();
        }
    }
}
