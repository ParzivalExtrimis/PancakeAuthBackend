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
        private readonly ILogger<AdminController> Log;
        public AdminController(IAdminService AdminService, ILoggerFactory loggerFactory) {
            Log = loggerFactory.CreateLogger<AdminController>();
            _AdminService = AdminService;
        }

        //--------------------------------School-------------------------------------------------

        //GET schools
        [HttpGet("School")]
        async public Task<IActionResult> GetSchools() {
            var schools = await _AdminService.GetSchools();
            if(schools.Count == 0) {
                return new NotFoundObjectResult("No Schools Found.");
            }
            return new OkObjectResult(schools);          
        }

        [HttpGet("School/Paged")]
        async public Task<IActionResult> GetSchoolsByPage(int pageIndex, int pageSize) {
            var schools = await _AdminService.GetSchoolsByPage(pageIndex, pageSize);
            if (schools.Count == 0) {
                return new NotFoundObjectResult("No Schools Found.");
            }
            return new OkObjectResult(schools);
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
                    Log.LogError("SchoolController - AddSchool()", errorMessage);
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


        //--------------------------------Subscriptions-------------------------------------------------

        //GET subscriptions
        [HttpGet("Subscriptions")]
        async public Task<IActionResult> GetSubscriptions() {
            var subscriptions = await _AdminService.GetSubscriptions();
            if (subscriptions.Count == 0) {
                return new NotFoundObjectResult("No Subscriptions Found.");
            }
            return new OkObjectResult(subscriptions);
        }

        [HttpGet("Subscriptions/Paged")]
        async public Task<IActionResult> GetSubscriptionsByPage(int pageIndex, int pageSize) {
            var subscriptions = await _AdminService.GetSubscriptionsByPage(pageIndex, pageSize);
            if (subscriptions.Count == 0) {
                return new NotFoundObjectResult("No Subscriptions Found.");
            }
            return new OkObjectResult(subscriptions);
        }

        //POST school
        [HttpPost("Subscriptions")]
        async public Task<IActionResult> AddSubscription([FromBody] SubscriptionDTO subscription) {

            try {
                return await _AdminService.AddSubscription(subscription)
                    ? new OkObjectResult($"Subscription [{subscription.Name}] Added Successfully")
                    : new BadRequestObjectResult($"Subscription [{subscription.Name}] could NOT be added. Check parameters are correct.");
            }
            catch (DbUpdateException ex) {
                // Handle the exception
                if (ex.InnerException is SqlException sqlEx && sqlEx.Number == 2601) {
                    string errorMessage = $"Failed: {sqlEx.Message}";
                    return new BadRequestObjectResult(errorMessage);
                }
                return new BadRequestObjectResult($"Subscription [{subscription.Name}] could NOT be added. Constraints violated");
            }
        }

        //EDIT School
        [HttpPut("Subscriptions/{subscriptionName}")]
        public async Task<IActionResult> EditSubscription(string subscriptionName, [FromBody] SubscriptionDTO subscription) {
            try {
                return await _AdminService.UpdateSubscription(subscriptionName, subscription)
              ? new OkObjectResult("Subscription Updated Successfully")
              : new BadRequestObjectResult("Subscription could not be updated. Provide a correctly formatted JSON.");
            }
            catch (DbUpdateException ex) {
                // Handle the exception
                if (ex.InnerException is SqlException sqlEx && sqlEx.Number == 2601) {
                    string errorMessage = $"Failed: {sqlEx.Message}";
                    return new BadRequestObjectResult(errorMessage);
                }
                return new BadRequestObjectResult($"Subscription could not be updated. Constraints violated \n {ex.Message}" +
                    $"\n {(ex.InnerException ?? ex).Message}");
            }
        }

        //DELETE school
        [HttpDelete("Subscription/{subscriptionName}")]
        async public Task<IActionResult> DeleteSubscription(string subscriptionName) {
            return await _AdminService.DeleteSubscription(subscriptionName)
                ? new OkObjectResult($"Subscription [ {subscriptionName} ] Deleted Successfully.")
                : new BadRequestObjectResult($"Subscription [ {subscriptionName} ] could not be Deleted. Provide a correctly formatted JSON.");
        }

        //--------------------------------Chapters-------------------------------------------------

        //GET Chapters
        [HttpGet("Chapters")]
        public IActionResult GetChapters() {
            var subscriptions = _AdminService.GetChapters();
            if (subscriptions.Count == 0) {
                return new NotFoundObjectResult("No Chapters Found.");
            }
            return new OkObjectResult(subscriptions);
        }

        [HttpGet("Chapters/Paged")]
        public IActionResult GetChaptersByPage(int pageIndex, int pageSize) {
            var subscriptions = _AdminService.GetChaptersByPage(pageIndex, pageSize);
            if (subscriptions.Count == 0) {
                return new NotFoundObjectResult("No Chapters Found.");
            }
            return new OkObjectResult(subscriptions);
        }

        //GET chapter[name]
        [HttpGet("Chapter/{chapterName}")]
        async public Task<IActionResult> FindChapter(string chapterName) {
            var chapter = await _AdminService.FindChapter(chapterName);
            return chapter is not null
                ? new OkObjectResult(chapter)
                : new BadRequestObjectResult($"Chapter [ {chapterName} ] could not be found. Provide a correctly formatted JSON.");
        }

        //POST chapter
        [HttpPost("Chapters")]
        async public Task<IActionResult> AddChapters([FromBody] List<ChapterDTO> chapters) {

            try {
                return await _AdminService.AddChapters(chapters)
                    ? new OkObjectResult($"Chapter(s) Added Successfully")
                    : new BadRequestObjectResult($"Chapter(s) could NOT be added. Check parameters are correct.");
            }
            catch (DbUpdateException ex) {
                // Handle the exception
                if (ex.InnerException is SqlException sqlEx && sqlEx.Number == 2601) {
                    string errorMessage = $"Failed: {sqlEx.Message}";
                    return new BadRequestObjectResult(errorMessage);
                }
                return new BadRequestObjectResult($"Chapter(s) could NOT be added. Constraints violated");
            }
        }

        //DELETE school
        [HttpDelete("Chapter/{chapterName}")]
        async public Task<IActionResult> DeleteChapter(string chapterName) {
            return await _AdminService.DeleteChapter(chapterName)
                ? new OkObjectResult($"Chapter [ {chapterName} ] Deleted Successfully.")
                : new BadRequestObjectResult($"Subscription [ {chapterName} ] could not be Deleted. Provide a correctly formatted JSON.");
        }
    }
}
