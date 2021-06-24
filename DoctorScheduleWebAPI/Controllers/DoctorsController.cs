using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DoctorScheduleWebAPI.Models;
using DoctorScheduleWebAPI.Repositories;


namespace DoctorScheduleWebAPI.Controllers
{
    [ApiController]
    
    public class DoctorsController : ControllerBase
    {
        private readonly DoctorRepository _doctorRepository;
        public DoctorsController(DoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        [HttpGet("api/doctor")]
        public async Task<IActionResult> GetDoctorAsync([FromQuery] int id)
        {
            var doctor =  await _doctorRepository.GetByIdAsync(id);
            if (doctor == null)
            {
                return  new NotFoundResult();
            }      
            
            return new OkObjectResult(doctor);
        }

        [HttpPost("api/doctor")]
        public async Task<IActionResult> CreateDoctorAsync([FromBody]Doctor doctor)
        {
            doctor = await _doctorRepository.CreateDoctorAsync(doctor.FirstName, doctor.LastName, doctor.Speciality);
            
            return new OkObjectResult(doctor);
        }

        [HttpDelete("api/doctor")]
        public async Task<IActionResult> DeleteByIdAsync([FromQuery] int id)
        {
            await _doctorRepository.DeleteByIdAsync(id);
            
            return new OkResult();
        }

        [HttpPut("api/doctor")]
        public async Task<IActionResult> UpdateByIdAsync([FromBody] Doctor doc)
        {
            var doctor = await _doctorRepository.UpdateDoctorByIdAsync(doc.Id, doc.FirstName, doc.LastName, doc.Speciality);
            if (doctor == null)
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(doctor);
        }
    }
}
