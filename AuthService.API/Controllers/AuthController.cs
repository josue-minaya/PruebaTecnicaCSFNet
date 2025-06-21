using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AuthService.API.ApiResponse;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace AuthService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IConfiguration _config;
        private readonly ILogger<AuthController> _logger;


        public AuthController(IConfiguration config, ILogger<AuthController> logger)
        {
            _config = config;
            _logger = logger;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody]LoginDto login)
        {
            _logger.LogInformation("Intento de login para usuario: {Username}", login.Username);

            try
            {
                if (login.Username == "admin" && login.Password == "123")
                {
                    var token = GenerateJwtToken(login.Username);
                    _logger.LogInformation("Login exitoso para usuario: {Username}", login.Username);
                    return Ok(ApiResponse<string>.Ok(token, "Login exitoso"));
                }

                _logger.LogInformation("Credenciales inválidas para usuario: {Username}", login.Username);
                return Unauthorized(ApiResponse<string>.Fail(new List<string> { "Credenciales inválidas" }));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado durante el login para usuario: {Username}", login.Username);
                return StatusCode(500, ApiResponse<string>.Fail(new List<string> { "Error interno del servidor" }));
            }
        }


        private string GenerateJwtToken(string username)
        {
            var jwtSettings = _config.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new[]
                {
                new Claim("username", username),
                new Claim(ClaimTypes.Role, "Admin")
            }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                Issuer = jwtSettings["Issuer"],
                Audience = jwtSettings["Audience"],
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}