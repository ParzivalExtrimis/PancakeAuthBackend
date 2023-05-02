using Microsoft.AspNetCore.Mvc;
using PancakeAuthBackend.Services;

namespace PancakeAuthBackend.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase {
        private readonly IAccountService _accountService;
        private readonly ILogger<AdminController> _log;
        public AccountController(IAccountService accountService, ILoggerFactory loggerFactory) {
            _log = loggerFactory.CreateLogger<AdminController>();
            _accountService = accountService;
        }

        //GET Token
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("Token")]
        async public Task<IActionResult> GetToken([FromBody] LoginDTO loginDetails) {
            if(await _accountService.SignIn(loginDetails)) {
                try {
                    return Ok(await _accountService.GetToken());
                }
                catch(Exception ex) {
                    _log.LogError("Login Token generator crashed unexplicably. \n {ex.Message} \n {ex.InnerException}", ex.Message, ex.InnerException);
                    return Problem(detail: "Login Unsuccessful. Internal Service Error", statusCode: 500);
                }
            }
            _log.LogInformation("Account-Service Login-Failed, Could not create token : [{loginDetails.UserName}]", loginDetails.UserName);
            return BadRequest();
        }
    }
}
