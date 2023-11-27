using Entities.DataTransferObjects.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationManager;

        public AuthenticationController(IAuthenticationService authenticationManager)
        {
            _authenticationManager = authenticationManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistrationDto)
        {
            var result = new IdentityResult();
            try
            {
                result = await _authenticationManager.RegisterUser(userForRegistrationDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }
            return CreatedAtRoute("GetUser", new { userName = userForRegistrationDto.UserName }, null);
        }
    }
}
