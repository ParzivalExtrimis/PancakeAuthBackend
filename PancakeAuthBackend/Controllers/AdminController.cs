using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PancakeAuthBackend.Services;

namespace PancakeAuthBackend.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase {
        private readonly IAdminService _AdminService;
        public AdminController(IAdminService AdminService) {
            _AdminService = AdminService;
        }

        //POST school
        [HttpPost("School")]
        async public Task<IActionResult> AddSchool([FromBody] SchoolDTO school) {

            try {
                return await _AdminService.AddSchool(school)
                    ? new OkObjectResult($"School [{school.Name}] Added Successfully")
                    : new BadRequestObjectResult($"School [{school.Name}] could NOT be added. Check parameters are correct.");
            }
            catch (DbUpdateException ex) {
                // Handle the exception
                if (ex.InnerException is SqlException sqlEx && sqlEx.Number == 2601) {
                    string errorMessage = $"Failed: {sqlEx.Message}";
                    return new BadRequestObjectResult(errorMessage);
                }
                return new BadRequestObjectResult($"School [{school.Name}] could NOT be added. Constraints violated");
            }
        }

        //EDIT School
        [HttpPut("School/{schoolName}")]
        public async Task<IActionResult> EditSchool(string schoolName, [FromBody] SchoolDTO school) {
            if (!await _AdminService.SchoolExists(schoolName)) {
                return new NotFoundObjectResult("School does not exist.");
            }

            try {
                return await _AdminService.EditSchool(schoolName, school)
              ? new OkObjectResult("School Updated Successfully")
              : new BadRequestObjectResult("School could not be updated. Provide a correctly formatted JSON.");
            }
            catch (DbUpdateException ex) {
                // Handle the exception
                if (ex.InnerException is SqlException sqlEx && sqlEx.Number == 2601) {
                    string errorMessage = $"Failed: {sqlEx.Message}";
                    return new BadRequestObjectResult(errorMessage);
                }
                return new BadRequestObjectResult($"School could not be updated. Constraints violated \n {ex.Message}" +
                    $"\n {(ex.InnerException ?? ex).Message}");
            }
        }

        //DELETE school

        [HttpDelete("School/{schoolName}")]
        async public Task<IActionResult> DeleteSchool(string schoolName) {
            if (!await _AdminService.SchoolExists(schoolName)) {
                return new NotFoundObjectResult("School does not exist.");
            }

            return await _AdminService.DeleteSchool(schoolName)
                ? new OkObjectResult($"School [ {schoolName} ] Deleted Successfully.")
                : new BadRequestObjectResult($"School [ {schoolName} ] could not be Deleted. Provide a correctly formatted JSON.");
        }

    }
}
