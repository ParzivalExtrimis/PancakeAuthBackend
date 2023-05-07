using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using PancakeAuthBackend.Data;
using PancakeAuthBackend.Services;
using System.Reflection.Metadata.Ecma335;

namespace PancakeAuthBackend.Controllers {
    [Route("api/[controller]")]
    [Authorize(Policy = "SchoolAccess")]
    [ApiController]
    public class SchoolController : ControllerBase {
        private readonly ISchoolService _schoolService;
        public SchoolController(ISchoolService schoolService) {
            _schoolService = schoolService;
        }

        // GET: school.self => when jwt is setup, now {name}
        [HttpGet("{name}")]
        async public Task<IActionResult> Get(string name) {
            return await _schoolService.SchoolExists(name)
                ? new OkObjectResult(await _schoolService.GetSchoolData(name))
                : new NotFoundObjectResult("School does not exist.");
        }

        //GET: school.self.AllStudents => list
        [HttpGet("{schoolName}/Students")]
        async public Task<IActionResult> ListStudents(string schoolName) {
            if (!await _schoolService.SchoolExists(schoolName)) {
                return new NotFoundObjectResult("School does not exist.");
            }
            var students = await _schoolService.GetSchoolStudents(schoolName);
            return students.Count > 0
                ? new OkObjectResult(students)
                : new NotFoundObjectResult("No Students found.");
        }

        //GET: school.self.Grade.Students => list
        [HttpGet("{schoolName}/Students/{department}")]
        async public Task<IActionResult> ListStudentsByGrade(string schoolName, string department) {
            if (!await _schoolService.SchoolExists(schoolName)) {
                return new NotFoundObjectResult("School does not exist.");
            }
            var students = await _schoolService.GetSchoolStudentByDepartment(schoolName: schoolName, department: department);
            return students.Count > 0
                ? new OkObjectResult(students)
                : new NotFoundObjectResult("No Students found.");
        }

        //GET: school.self.AllStudents.page => list(page)
        [HttpGet("{schoolName}/Students/Paged")]
        async public Task<IActionResult> ListStudentsPaged(string schoolName, int pageIndex, int pageSize) {
            if (!await _schoolService.SchoolExists(schoolName)) {
                return new NotFoundObjectResult("School does not exist.");
            }
            var students = await _schoolService.GetSchoolStudentsByPage(schoolName, pageIndex, pageSize);
            return students.Count > 0
                ? new OkObjectResult(students)
                : new NotFoundObjectResult("No Students found.");
        }

        //GET: school.self.Grade.Students.page => list(page)
        [HttpGet("{schoolName}/Students/{department}/Paged")]
        async public Task<IActionResult> ListStudentsByGradePaged(string schoolName, string department, int pageIndex, int pageSize) {
            if (!await _schoolService.SchoolExists(schoolName)) {
                return new NotFoundObjectResult("School does not exist.");
            }
            var students = await _schoolService.GetSchoolStudentByDepartmentPaged(
                schoolName: schoolName,
                department: department,
                pageIndex, pageSize);
            return students.Count > 0
                ? new OkObjectResult(students)
                : new NotFoundObjectResult("No Students found.");
        }

        // POST api/<School>
        [HttpPost("{schoolName}/Students")]
        public async Task<IActionResult> AddStudents(string schoolName, [FromBody] StudentDTO[] students) {
            if (!await _schoolService.SchoolExists(schoolName)) {
                return new NotFoundObjectResult("School does not exist.");
            }

            var studentList = new List<StudentDTO>(students);
            try {
                return await _schoolService.AddStudents(schoolName, studentList)
              ? new OkObjectResult("Student(s) Added Successfully")
              : new BadRequestObjectResult("Students could not be added. Provide a correctly formatted JSON.");
            }
            catch (DbUpdateException ex) {
                // Handle the exception
                if (ex.InnerException is SqlException sqlEx && sqlEx.Number == 2601) {
                    string errorMessage = $"Failed: {sqlEx.Message}";
                    return new BadRequestObjectResult(errorMessage);
                }
                return new BadRequestObjectResult("Students could not be added. Constraints violated");
            }
        }


        // PUT api/<ValuesController>/5
        [HttpPut("{schoolName}/Students")]
        public async Task<IActionResult> EditStudent(string schoolName, string SSID, [FromBody] StudentDTO student) {
            if (!await _schoolService.SchoolExists(schoolName)) {
                return new NotFoundObjectResult("School does not exist.");
            }

            try {
                return await _schoolService.EditStudent(student, SSID, schoolName)
              ? new OkObjectResult("Student Attributes Updated Successfully")
              : new BadRequestObjectResult("Student could not be updated. Provide a correctly formatted JSON.");
            }
            catch (DbUpdateException ex) {
                // Handle the exception
                if (ex.InnerException is SqlException sqlEx && sqlEx.Number == 2601) {
                    string errorMessage = $"Failed: {sqlEx.Message}";
                    return new BadRequestObjectResult(errorMessage);
                }
                return new BadRequestObjectResult("Students could not be updated. Constraints violated");
            }
        }

        //DELETE: school.self.sudent { name }
        [HttpDelete("{schoolName}/Students/{SUID}")]
        async public Task<IActionResult> DeleteStudent(string schoolName, string SUID) {
            if (!await _schoolService.SchoolExists(schoolName)) {
                return new NotFoundObjectResult("School does not exist.");
            }
            
            return await _schoolService.DeleteStudent(SUID, schoolName)
                ? new OkObjectResult("Student Deleted Successfully.")
                : new BadRequestObjectResult("Student could not be Deleted. Provide a correctly formatted JSON.");
        }


    }
}
