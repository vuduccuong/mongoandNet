using App.Common;
using App.Domain.Entities;
using App.Domain.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace App.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _config;

        public UsersController(IUserService userService, IConfiguration config)
        {
            _userService = userService;
            _config = config;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserEntity user)
        {
            if (user == null)
            {
                return BadRequest(ModelState);
            }

            var userSameEmail = await _userService.FinByEmail(user.Email);

            if (userSameEmail != null)
            {
                ModelState.AddModelError("Error", "User đã tồn tại!");
                return StatusCode(StatusCodes.Status404NotFound, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                await _userService.Register(user);
                return CreatedAtRoute("Login", user);
                //return StatusCode(StatusCodes.Status201Created);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
            }
        }

        [HttpPost(Name = "Login")]
        public async Task<IActionResult> Login([FromBody] UserEntity user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            var userEmail = await _userService.FinByEmail(user.Email);
            if (userEmail == null)
            {
                return NotFound();
            }
            try
            {
                var token = await TokenHelpers.GenerateJWT(userEmail, _config["Jwt:Key"], _config["Jwt:Issuer"]);
                Response.Cookies.Append("X-Access-Token", token, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict });
                Response.Cookies.Append("X-Username", userEmail.UserName, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict });
                
                return Ok(token);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, user);
            }
        }
    }
}