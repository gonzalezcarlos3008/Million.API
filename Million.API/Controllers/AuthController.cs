using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;
using System.Text;

namespace Million.API.Controllers
{
    /// <summary>
    /// Controller for authentication operations.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthController"/> class.
        /// </summary>
        /// <param name="configuration">The configuration containing authentication settings.</param>
        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Logs in a user and generates a JWT token.
        /// </summary>
        /// <returns>A response containing the generated JWT token.</returns>
        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Authenticate user", Description = "Generates a JWT token for authentication")]
        public IActionResult Login()
        {
            // Simulated token generation
            var token = GenerateJwtToken();
            return Ok(new { token });
        }

        /// <summary>
        /// Generates a JWT token.
        /// </summary>
        /// <returns>A JWT token string.</returns>
        private string GenerateJwtToken()
        {
            var secretKey = _configuration["Jwt:SecretKey"];
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new System.IdentityModel.Tokens.Jwt.JwtSecurityToken(
                issuer: "millions.co",
                audience: "millions.co",
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
