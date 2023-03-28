using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;
using PancakeAuthBackend.Data;
using PancakeAuthBackend.Services;
using System.Reflection.Metadata.Ecma335;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PancakeAuthBackend.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolController : ControllerBase {
        private readonly ISchoolService _schoolService;
        public SchoolController(ISchoolService schoolService) {
            _schoolService = schoolService;
        }

        // GET: school.self => when jwt is setup, now {id}
        [HttpGet("{name}")]
        public IActionResult Get(string name) {
            return _schoolService.SchoolExists(name)
                ? new OkObjectResult(_schoolService.GetSchoolData(name))
                : new NotFoundObjectResult("School does not exist.");
        }

        //GET: school.self.AllStudents => list
        [HttpGet("{schoolName}/Students")]
        public IActionResult ListStudents(string schoolName) {
            if (!_schoolService.SchoolExists(schoolName)) {
                return new NotFoundObjectResult("School does not exist.");
            }
            var students = _schoolService.GetSchoolStudents(schoolName);
            return students.Count > 0
                ? new OkObjectResult(students)
                : new NotFoundObjectResult("No Students found.");
        }

        //GET: school.self.AllStudents.page => list(page)
        [HttpGet("{schoolName}/Students/Paged")]
        public IActionResult ListStudents(string schoolName, int pageIndex, int pageSize) {
            if (!_schoolService.SchoolExists(schoolName)) {
                return new NotFoundObjectResult("School does not exist.");
            }
            var students = _schoolService.GetSchoolStudentsByPage(schoolName, pageIndex, pageSize);
            return students.Count > 0
                ? new OkObjectResult(students)
                : new NotFoundObjectResult("No Students found.");
        }

        [HttpGet("{schoolName}/Subscriptions")]
        public IActionResult ListSubscriptions(string schoolName) {
            if (!_schoolService.SchoolExists(schoolName)) {
                return new NotFoundObjectResult("School does not exist.");
            }
            var subscriptions = _schoolService.GetSchoolSubscriptions(schoolName);
            return subscriptions.Count > 0
                ? new OkObjectResult(subscriptions)
                : new NotFoundObjectResult("No Subscriptions found.");
        }

        [HttpGet("{schoolName}/Subscriptions/Paged")]
        public IActionResult ListSubscriptionsByPage(string schoolName, int pageIndex, int pageSize) {
            if (!_schoolService.SchoolExists(schoolName)) {
                return new NotFoundObjectResult("School does not exist.");
            }
            var subscriptions = _schoolService.GetSchoolSubscriptionsByPage(schoolName, pageIndex, pageSize);
            return subscriptions.Count > 0
                ? new OkObjectResult(subscriptions)
                : new NotFoundObjectResult("No Subscriptions found.");
        }

        [HttpGet("{schoolName}/Batches")]
        public IActionResult ListBatches(string schoolName) {
            if (!_schoolService.SchoolExists(schoolName)) {
                return new NotFoundObjectResult("School does not exist.");
            }
            var batches = _schoolService.GetSchoolBatches(schoolName);
            return batches.Count > 0
                ? new OkObjectResult(batches)
                : new NotFoundObjectResult("No Subscriptions found.");
        }

        [HttpGet("{schoolName}/Batches/Paged")]
        public IActionResult ListBatchesbyPage(string schoolName, int pageIndex, int pageSize) {
            if (!_schoolService.SchoolExists(schoolName)) {
                return new NotFoundObjectResult("School does not exist.");
            }
            var batches = _schoolService.GetSchoolBatchesByPage(schoolName, pageIndex, pageSize);
            return batches.Count > 0
                ? new OkObjectResult(batches)
                : new NotFoundObjectResult("No Subscriptions found.");
        }

        [HttpGet("{schoolName}/Payments")]
        public IActionResult ListPayments(string schoolName) {
            if (!_schoolService.SchoolExists(schoolName)) {
                return new NotFoundObjectResult("School does not exist.");
            }
            var payments = _schoolService.GetSchoolPayments(schoolName);
            return payments.Count > 0
                ? new OkObjectResult(payments)
                : new NotFoundObjectResult("No Payments found.");
        }
        [HttpGet("{schoolName}/Payments/Paged")]
        public IActionResult ListPaymentsByPage(string schoolName, int pageIndex, int pageSize) {
            if (!_schoolService.SchoolExists(schoolName)) {
                return new NotFoundObjectResult("School does not exist.");
            }
            var payments = _schoolService.GetSchoolPaymentsByPage(schoolName, pageIndex, pageSize);
            return payments.Count > 0
                ? new OkObjectResult(payments)
                : new NotFoundObjectResult("No Payments found.");
        }


        // POST api/<ValuesController>
        [HttpPost("{schoolName}/Students")]
        public async Task<IActionResult> AddStudents(string schoolName, [FromBody] StudentDTO[] students) {
            if (!_schoolService.SchoolExists(schoolName)) {
                return new NotFoundObjectResult("School does not exist.");
            }

            var studentList = new List<StudentDTO>(students);
            return await _schoolService.AddStudents(studentList, schoolName)
                ? new OkObjectResult("Student Added Successfully")
                : new BadRequestObjectResult("Students could not be added. Provide a correct formatted JSON.");
        }


        // POST api/<ValuesController>
        [HttpPost("{schoolName}/Batches")]
        public async Task<IActionResult> AddBatches(string schoolName, [FromBody] BatchDTO batch) {
            if (!_schoolService.SchoolExists(schoolName)) {
                return new NotFoundObjectResult("School does not exist.");
            }

            return await _schoolService.AddBatch(batch, schoolName)
                ? new OkObjectResult("Batch Added Successfully")
                : new BadRequestObjectResult("Batch could not be added. Provide a correctly formatted JSON.");
        }


        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value) {
        }
    }
}
