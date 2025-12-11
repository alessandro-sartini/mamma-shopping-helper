using AuthService.Models.DTOs;
using AuthService.Services;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        /// <summary>
        /// Endpoint per registrare un nuovo utente
        /// POST /api/auth/register
        /// </summary>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            // 1. Validazione automatica dei Data Annotations
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Registrazione fallita: validazione dati non valida");
                return BadRequest(ModelState);
            }

            // 2. Chiama il servizio di autenticazione
            var response = await _authService.RegisterAsync(registerDto);

            // 3. Se response è null = username o email già esistenti
            if (response == null)
            {
                _logger.LogWarning("Registrazione fallita: username o email già esistenti");
                return BadRequest(new { message = "Username o email già esistenti" });
            }

            // 4. Registrazione riuscita, ritorna token JWT
            _logger.LogInformation($"Nuovo utente registrato: {response.Username}");
            return Ok(response);
        }

        /// <summary>
        /// Endpoint per login utente esistente
        /// POST /api/auth/login
        /// </summary>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            // 1. Validazione automatica
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Login fallito: validazione dati non valida");
                return BadRequest(ModelState);
            }

            // 2. Chiama il servizio di autenticazione
            var response = await _authService.LoginAsync(loginDto);

            // 3. Se response è null = credenziali errate
            if (response == null)
            {
                _logger.LogWarning($"Login fallito per: {loginDto.EmailOrUsername}");
                return Unauthorized(new { message = "Credenziali non valide" });
            }

            // 4. Login riuscito, ritorna token JWT
            _logger.LogInformation($"Login riuscito per: {response.Username}");
            return Ok(response);
        }

        /// <summary>
        /// Endpoint di test per verificare che il servizio è attivo
        /// GET /api/auth/health
        /// </summary>
        [HttpGet("health")]
        public IActionResult Health()
        {
            return Ok(new
            {
                status = "AuthService is running",
                timestamp = DateTime.UtcNow,
                version = "1.0.0"
            });
        }
    }
}
