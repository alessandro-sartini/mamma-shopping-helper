using AuthService.Data;
using AuthService.Models;
using AuthService.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthService.Services
{
    public class AuthServiceImpl : IAuthService
    {
        private readonly AuthDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthServiceImpl(AuthDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        /// <summary>
        /// Registra un nuovo utente
        /// </summary>
        public async Task<AuthResponseDto?> RegisterAsync(RegisterDto registerDto)
        {
            // 1. Verifica se username già esistente
            if (await _context.Users.AnyAsync(u => u.Username == registerDto.Username))
            {
                return null; // Username duplicato
            }

            // 2. Verifica se email già esistente
            if (await _context.Users.AnyAsync(u => u.Email == registerDto.Email))
            {
                return null; // Email duplicata
            }

            // 3. Hash della password con BCrypt
            // BCrypt genera AUTOMATICAMENTE un salt casuale e lo include nell'hash!
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);

            // 4. Crea nuovo utente
            var user = new User
            {
                Username = registerDto.Username,
                Email = registerDto.Email,
                PasswordHash = passwordHash, 
                DataRegistrazione = DateTime.UtcNow
            };

            // 5. Salva nel database
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // 6. Genera e ritorna il JWT token
            return GenerateJwtToken(user);
        }

        /// <summary>
        /// Autentica utente esistente
        /// </summary>
        public async Task<AuthResponseDto?> LoginAsync(LoginDto loginDto)
        {
            // 1. Cerca utente per email O username
            var user = await _context.Users
                .FirstOrDefaultAsync(u =>
                    u.Email == loginDto.EmailOrUsername ||
                    u.Username == loginDto.EmailOrUsername);

            // 2. Se utente non trovato
            if (user == null)
            {
                return null; // Credenziali errate
            }

            // 3. Verifica password con BCrypt
            // BCrypt.Verify estrae automaticamente il salt dall'hash salvato
            // e lo usa per verificare la password inserita
            bool isValidPassword = BCrypt.Net.BCrypt.Verify(
                loginDto.Password,      // Password in chiaro inserita
                user.PasswordHash       // Hash salvato nel DB (contiene salt!)
            );

            // 4. Se password errata
            if (!isValidPassword)
            {
                return null; // Password sbagliata
            }

            // 5. Genera e ritorna il JWT token
            return GenerateJwtToken(user);
        }

        /// <summary>
        /// Genera un JWT token per l'utente autenticato
        /// </summary>
        private AuthResponseDto GenerateJwtToken(User user)
        {
            // 1. Leggi configurazione JWT da appsettings.json
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["SecretKey"];
            var issuer = jwtSettings["Issuer"];
            var audience = jwtSettings["Audience"];
            var expirationMinutes = int.Parse(jwtSettings["ExpirationMinutes"]!);

            // 2. Crea claims (informazioni utente dentro il token)
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("userId", user.Id.ToString()),       // Custom claim
                new Claim("username", user.Username),           // Custom claim
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) // Token ID univoco
            };

            // 3. Crea chiave di firma (usando SecretKey)
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // 4. Calcola scadenza
            var expiration = DateTime.UtcNow.AddMinutes(expirationMinutes);

            // 5. Crea il token JWT
            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: expiration,
                signingCredentials: credentials
            );

            // 6. Serializza il token in stringa
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            // 7. Ritorna DTO con tutte le info
            return new AuthResponseDto
            {
                Token = tokenString,
                Username = user.Username,
                Email = user.Email,
                Expiration = expiration
            };
        }
    }
}
