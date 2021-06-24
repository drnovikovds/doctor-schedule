using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using DoctorScheduleWebAPI.Services;
using Newtonsoft.Json;
using DoctorScheduleWebAPI.Models.UserDto;

namespace DoctorScheduleWebAPI.Controllers
{
    [ApiController]
    public class GetCityByIpController : ControllerBase
    {
        private readonly IpCityService _ipCityService;
        public GetCityByIpController(IpCityService ipCityService)
        {
            _ipCityService = ipCityService;
        }

        [HttpPost("api/getcitybyip")]
        public async Task<IActionResult> GetCityByIdAsync ([FromBody] IpToGetCityModel ipToGetCityModel)
        {
            var city = await _ipCityService.GetCityByIpAsync(ipToGetCityModel);
            
            return new OkObjectResult(city);
        }
    }
}
