using IssueManagement.Application.Users.Interfaces;
using IssueManagement.Application.Users.Requests;
using IssueManagement.Application.Users.Responses;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace IssueManagement.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
 
        [HttpGet("email/{email}")]
        [SwaggerResponse(200, "User retrieved successfully", typeof(UserResponseModel))]
        public async Task<ActionResult<UserResponseModel>> GetUserByEmail(string email, CancellationToken cancellationToken)
        {
            var user = await _userService.GetByEmailAsync(email, cancellationToken);  
            return Ok(user);    
        }

        [HttpGet("username/{username}")]
        [SwaggerResponse(200, "User retrieved successfully", typeof(UserResponseModel))]
        public async Task<ActionResult<UserResponseModel>> GetUserByUsername(string username, CancellationToken cancellationToken)
        {
            var user = await _userService.GetByUsernameAsync(username, cancellationToken);
            return Ok(user);    
        }

        [HttpPut("update-user")]
        [SwaggerResponse(200, "User updated successfully", typeof(UserResponseModel))]
        public async Task<ActionResult<UserResponseModel>> UpdateUser([FromBody]UserUpdateModel updateModel, int id, CancellationToken cancellationToken)
        {
            var user = await _userService.UpdateAsync(updateModel, id, cancellationToken);
            return Ok(user);
        }
    }
}
