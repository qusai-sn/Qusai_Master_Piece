using MasterPiece.DTO;
using MasterPiece.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MasterPiece.Services
{


    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto);
        Task<AuthResponseDto> LoginAsync(LoginDto loginDto);
        Task<bool> ChangePasswordAsync(ResetPasswordDto resetPasswordDto);
        Task<bool> LogoutAsync(int userId);
        Task<bool> ValidateTokenAsync(string token);
    }



    public class AuthService : IAuthService
    {



        private readonly MasterPieceContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(MasterPieceContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }






        public async Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto)
        {


            // Check if email already exists
            if (await _context.Users.AnyAsync(u => u.Email == registerDto.Email))
            {
                throw new Exception("Email already registered");
            }



            // Generate salt and hash password
            var salt = GenerateSalt();
            var passwordHash = HashPassword(registerDto.Password, salt);



            // Create new user
            var user = new User
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                Username = registerDto.Username,
                PhoneNumber = registerDto.PhoneNumber,
                PasswordHash = passwordHash,
                Salt = salt,
                IsOrganizer = false
            };




            _context.Users.Add(user);
            await _context.SaveChangesAsync();




            // Generate JWT token
            var token = GenerateJwtToken(user);



            return new AuthResponseDto
            {
                UserId = user.UserId,
                Username = user.Username,
                Email = user.Email,
                Token = token,
                IsOrganizer = user.IsOrganizer ?? false
            };
        }





        public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
        {


            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == loginDto.Email);



            if (user == null)
            {
                throw new Exception("Invalid email or password");
            }



            // Verify password
            var passwordHash = HashPassword(loginDto.Password, user.Salt);
            if (passwordHash != user.PasswordHash)
            {
                throw new Exception("Invalid email or password");
            }



            // Generate JWT token
            var token = GenerateJwtToken(user);


            return new AuthResponseDto
            {
                UserId = user.UserId,
                Username = user.Username,
                Email = user.Email,
                Token = token,
                IsOrganizer = user.IsOrganizer ?? false
            };

        }






        public async Task<bool> ChangePasswordAsync(ResetPasswordDto resetPasswordDto)
        {


            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == resetPasswordDto.Email);



            if (user == null)
            {
                throw new Exception("User not found");
            }



            // Verify old password
            var oldPasswordHash = HashPassword(resetPasswordDto.OldPassword, user.Salt);
            if (oldPasswordHash != user.PasswordHash)
            {
                throw new Exception("Invalid current password");
            }



            // Generate new password hash
            var newSalt = GenerateSalt();
            var newPasswordHash = HashPassword(resetPasswordDto.NewPassword, newSalt);



            user.PasswordHash = newPasswordHash;
            user.Salt = newSalt;



            await _context.SaveChangesAsync();
            return true;
        }







        public async Task<bool> LogoutAsync(int userId)
        {
            // Implement any necessary logout logic
            // (e.g., invalidating tokens, updating user status)
            return true;
        }





        public async Task<bool> ValidateTokenAsync(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);

                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = _configuration["Jwt:Issuer"],
                    ValidAudience = _configuration["Jwt:Audience"],
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                return true;
            }
            catch
            {
                return false;
            }
        }




        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);



            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim("IsOrganizer", (user.IsOrganizer ?? false).ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"]
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }





        private string GenerateSalt()
        {
            var bytes = new byte[32];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(bytes);
            }
            return Convert.ToBase64String(bytes);
        }



        private string HashPassword(string password, string salt)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password + salt);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }
}