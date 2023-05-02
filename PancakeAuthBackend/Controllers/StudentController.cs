using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PancakeAuthBackend.Services;

namespace PancakeAuthBackend.Controllers {
    [ApiController]
    [Authorize(Policy = "StudentAccess")]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase {
        private readonly ILogger Log;
        private readonly BackendDataContext _db;
        public StudentController(BackendDataContext context, ILoggerFactory loggerFactory) {
            Log = loggerFactory.CreateLogger<StudentController>();
            _db = context;
        }

        [HttpGet("{ssid}")]
        async public Task<IActionResult> Get(string ssid) {
            Log.LogInformation("Student Controller", $"Student access requested [ {ssid} ]");
            var student = await _db.Students
                .Where(s => s.StudentUID == ssid)
                .Include(s => s.School)
                .Include(s => s.Grade)
                .Include(s => s.Batch)
                .Include(s => s.User)
                .Select(s => new StudentDTO {
                    StudentUID = s.StudentUID,
                    FirstName = s.User.FirstName,
                    LastName = s.User.LastName,
                    Name = s.Name,
                    Email = s.Email,
                    PhoneNumber = s.PhoneNumber,
                    CityOfOrigin = s.CityOfOrigin,
                    StateOfOrigin = s.StateOfOrigin,
                    CountryOfOrigin = s.CountryOfOrigin,
                    Nationality = s.Nationality,
                    School = s.School.Name,
                    Grade = s.Grade.Name,
                    Batch = s.Batch != null ? s.Batch.Name : "None"
                })
                .FirstOrDefaultAsync();

            if(student == null) {
                Log.LogError("Student Controller", $"Student [{ssid}] wasn't found");
                return NotFound();
            }
            Log.LogInformation("Student Controller", $"Student [{ssid}] retrieved");
            return Ok(student);
        }
    }
}
