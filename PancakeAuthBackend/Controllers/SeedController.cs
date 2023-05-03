using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace PancakeAuthBackend.Controllers {
    [Authorize(Policy = "AdminAccess")]
    [Route("api/[controller]")]
    [ApiController]
    public class SeedController : ControllerBase {
        private readonly ISampleDataSeeder _seeder;
        private readonly ILogger<SeedController> _logger;
        public SeedController(ILogger<SeedController> logger, ISampleDataSeeder seeder) {
            _logger = logger;
            _seeder = seeder;
        }
        
        [HttpPost]
        async public Task<IActionResult> Seed() {
            try {
                if (await _seeder.SeedAsync() > 0) {
                    _logger.LogInformation("Seed Successful");
                    return Ok("Seed Successful");
                }
                _logger.LogInformation("Seed did not add any new entries");
                return Problem(title: "Seed did not add any new entries", statusCode: 500);
            }
            catch (Exception ex) {
                _logger.LogError("Error: {0}", ex.Message);
                return Problem(title: ex.Message, statusCode: 500);
            }
        }
    }
}
