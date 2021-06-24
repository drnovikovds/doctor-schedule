using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DoctorScheduleWebAPI.Services;
using DoctorScheduleWebAPI.Repositories;
using DoctorScheduleWebAPI.Models.UserDto;

namespace DoctorScheduleWebAPI.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly SmsSenderService _smsSender;
        private readonly AuthSmsRepository _authSmsRepository;

        public AuthController(SmsSenderService smsSender, AuthSmsRepository authSmsRepository)
        {
            _smsSender = smsSender;
            _authSmsRepository = authSmsRepository;
        }

        [HttpPost("api/auth")]
        public async Task<IActionResult> AuthAsync([FromBody] AuthModel authModel)
        {
            var smsCode = _authSmsRepository.AddNumberAndCode(authModel.Number);
            var result = await _smsSender.SendSmsAsync(smsCode);
            return result ? new OkResult() : new StatusCodeResult(500);
        }

        [HttpPost("api/auth/confirm")]
        public IActionResult ConfirmNumber([FromBody] ConfirmModel confirmModel)
        {
            if (_authSmsRepository.IsValidCode(confirmModel.Number, confirmModel.SmsCode)) 
            {
                return new OkObjectResult("Number is valid");
            }

            return new UnauthorizedObjectResult("Number is not valid");
        }
    }
}
