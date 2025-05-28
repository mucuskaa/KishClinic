using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using KishClinic.Entities;
using KishClinic.Models;
using KishClinic.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace KishClinic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(RegisterDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await authService.RegisterAsync(request);
            if (user is null)
                return BadRequest("Email already exist.");

            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(LoginDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var token = await authService.LoginAsync(request);
            if (token is null)
                return BadRequest("Invalid Email or Password.");

            return Ok(token);
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<ActionResult<UserProfileDto>> GetCurrentUser([FromServices] IUserService userService)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
                return Unauthorized("Invalid user ID in token.");

            var user = await userService.GetByIdAsync(userId);
            if (user is null)
                return NotFound("User not found.");

            var profile = new UserProfileDto
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Phone = user.Phone,
                Address = user.Address,
                Notes = user.Notes,
                DateOfBirth = user.DateOfBirth,
                Role = user.Role
            };

            return Ok(profile);
        }

        [Authorize]
        [HttpGet]
        public IActionResult AuthenticatedOnlyEndpoint()
        {
            return Ok("You are authenticated!");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("admin-only")]
        public IActionResult AdminOnlyEndpoint()
        {
            return Ok("You are an admin!");
        }

        [Authorize]
        [HttpPut("profile")]
        public async Task<ActionResult<UserProfileDto>> UpdateProfile([FromBody] UserProfileDto request, [FromServices] IUserService userService)
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
                return Unauthorized("Invalid user ID in token.");

            if (userId != request.Id)
                return BadRequest("Cannot update profile for different user.");

            var user = await userService.GetByIdAsync(userId);
            if (user is null)
                return NotFound("User not found.");

            user.Email = request.Email;
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.DateOfBirth = request.DateOfBirth;
            user.Phone = request.Phone;
            user.Address = request.Address;
            user.Notes = request.Notes;

            var updatedUser = await userService.UpdateAsync(user);
            if (updatedUser is null)
                return BadRequest("Failed to update profile.");

            var profile = new UserProfileDto
            {
                Id = updatedUser.Id,
                Email = updatedUser.Email,
                FirstName = updatedUser.FirstName,
                LastName = updatedUser.LastName,
                Phone = updatedUser.Phone,
                Address = updatedUser.Address,
                Notes = updatedUser.Notes,
                DateOfBirth = updatedUser.DateOfBirth,
                Role = updatedUser.Role
            };

            return Ok(profile);
        }
    }
}
