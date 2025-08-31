using IssueManagement.Application.Accounts.Interfaces;
using IssueManagement.Application.Accounts.Requests;
using IssueManagement.Application.Users.Responses;
using IssueManagement.Infrastructure.Auth;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace IssueManagement.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IAccountService _accountService;

        public AccountController(IConfiguration configuration, IAccountService accountService)
        {
            _configuration = configuration;
            _accountService = accountService;
        }

        [HttpPost("authenticate")]
        [SwaggerOperation(Summary = "Logs in a user")]
        [SwaggerResponse(200, "User logged in successfully")]
        public async Task<IActionResult> authenticate([FromBody] LoginRequestModel loginRequest)
        {
            var user = await _accountService.AuthenticateAsync(loginRequest);
            return Ok(new
            {
                accessToken = JWTAuth.GenerateJwt(user, _configuration)
            });
        }

        [HttpPost("register")]
        [SwaggerResponse(200, "User registered successfully")]
        public async Task<ActionResult<UserResponseModel>> RegisterUser([FromBody] RegisterRequestModel loginRequest)
        {
            var user = await _accountService.RegisterAsync(loginRequest);
            return Ok(user);
        }

        [HttpPost("EmailConfirmation")]
        public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailRequest confirmEmailRequest)
        {
            await _accountService.ConfirmEmail(confirmEmailRequest);
            return Ok();
        }

        [HttpPost("change-pass")]
        public async Task<ActionResult<UserResponseModel>> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            var user = await _accountService.ChangePasswordAsync(request);
            return Ok(user);
        }

        [HttpPost("reset-password")]
        public async Task<ActionResult> ResetPasswordAsync([FromBody] ResetPasswordRequest request)
        {
            await _accountService.ResetPasswordAsync(request);
            return Ok();
        }

        [HttpPost("NewPassword")]
        public async Task<ActionResult<UserResponseModel>> SetNewPasswordAsync([FromBody] NewPasswordRequest request)
        {
            var user = await _accountService.NewPasswordAsync(request);
            return Ok(user);
        }

        [HttpPost("resend-code")]
        public async Task<IActionResult> ResendCodeToConfirm([FromBody] ResendCodeRequest request)
        {
            await _accountService.ResendCode(request);
            return Ok();
        }
    }
}
