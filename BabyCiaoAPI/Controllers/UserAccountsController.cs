using System.Collections.Generic;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using BabyCiaoAPI.DTO;
using BabyCiaoAPI.Models;
using Microsoft.AspNetCore.Authorization;
using BCrypt.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace BabyCiaoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserAccountController : ControllerBase
    {
        private readonly BabyciaoContext _context;
        private readonly IConfiguration _configuration;

        public UserAccountController(BabyciaoContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] LoginDTO registerDTO)
        {
            if (_context.UserAccounts.Any(u => u.Account == registerDTO.Account))
            {
                return BadRequest(new { message = "帳號已重複" });
            }

            // 加密密碼
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerDTO.Password);

            var userAccount = new UserAccount
            {
                Account = registerDTO.Account,
                Password = hashedPassword,
                Permissions = registerDTO.Permissions  // 使用前端傳遞的權限值
            };

            _context.UserAccounts.Add(userAccount);
            _context.SaveChanges();

            return Ok(new { message = "註冊成功" });
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDTO loginDTO)
        {
            var userAccount = _context.UserAccounts
                .FirstOrDefault(u => u.Account == loginDTO.Account);

            if (userAccount == null || !BCrypt.Net.BCrypt.Verify(loginDTO.Password, userAccount.Password))
            {
                return Unauthorized(new { message = "登入失敗" });
            }

            var token = GenerateJwtToken(userAccount);
            var account = userAccount.Account;
            var hasBasicInfo = _context.UserInformations.Any(u => u.AccountUser == userAccount.Account);
            var permissions = userAccount.Permissions;
         
            return Ok(new { message = "登入成功", token, account, hasBasicInfo, permissions });
        }

        private string GenerateJwtToken(UserAccount user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.UserId.ToString()),
                    new Claim("Permissions", user.Permissions.ToString())  // 添加權限到聲明
                }),
                Expires = DateTime.UtcNow.AddMinutes(5),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        [HttpGet("{id}")]
        [Authorize]
        public IActionResult GetUser(int id)
        {
            var userAccount = _context.UserAccounts
                .FirstOrDefault(u => u.UserId == id);

            if (userAccount == null)
            {
                return NotFound(new { message = "無使用者" });
            }

            return Ok(userAccount);
        }
        [HttpGet]
        [Authorize]
        public IActionResult GetAllUsers()
        {
            var userAccounts = _context.UserAccounts.Select(u => new
            {
                u.UserId,
                u.Account,
                u.Permissions,
                UserInfo = _context.UserInformations
                    .Where(info => info.AccountUser == u.Account)
                    .Select(info => new
                    {
                        info.UserFirstName,
                        info.UserLastName
                    }).FirstOrDefault()
            }).ToList();

            return Ok(userAccounts);
        }

        [HttpPost("sendVerificationEmail")]
        public async Task<IActionResult> SendVerificationEmail([FromBody] EmailDTO emailDTO)
        {
            var userInformation = _context.UserInformations.FirstOrDefault(u => u.Email == emailDTO.Email);

            if (userInformation == null)
            {
                return BadRequest(new { message = "無效的電子郵件地址" });
            }

            var userAccount = _context.UserAccounts.FirstOrDefault(u => u.Account == userInformation.AccountUser);
            if (userAccount == null)
            {
                return BadRequest(new { message = "無效的帳號" });
            }

            var token = GenerateVerificationToken(userAccount);

            var verificationLink = $"https://localhost:7231/Login/resetpassword?token={token}";
            await SendEmailAsync(emailDTO.Email, "重設密碼驗證", $"請點擊此連結重設您的密碼: <a href=\"{verificationLink}\">點擊這裡</a>");

            return Ok(new { message = "已發送" });
        }

        private string GenerateVerificationToken(UserAccount user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.UserId.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            try
            {
                var smtpHost = _configuration["Smtp:Host"];
                var smtpPort = int.Parse(_configuration["Smtp:Port"]);
                var smtpUsername = _configuration["Smtp:Username"];
                var smtpPassword = _configuration["Smtp:Password"];
                var smtpFrom = _configuration["Smtp:From"];

                if (string.IsNullOrEmpty(smtpHost) || string.IsNullOrEmpty(smtpUsername) || string.IsNullOrEmpty(smtpPassword) || string.IsNullOrEmpty(smtpFrom))
                {
                    throw new ArgumentNullException("SMTP 設置不完整");
                }

                var smtpClient = new SmtpClient(smtpHost)
                {
                    Port = smtpPort,
                    Credentials = new System.Net.NetworkCredential(smtpUsername, smtpPassword),
                    EnableSsl = true // 確保啟用 SSL/TLS
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(smtpFrom),
                    Subject = subject,
                    Body = htmlMessage,
                    IsBodyHtml = true,
                };
                mailMessage.To.Add(email);

                await smtpClient.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                // 記錄錯誤信息
                Console.WriteLine($"發送郵件失敗: {ex.Message}");
                throw;
            }
        }

        [HttpPost("resetPassword")]
        public IActionResult ResetPassword([FromBody] ResetPasswordDTO resetPasswordDTO)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);

            try
            {
                var claimsPrincipal = tokenHandler.ValidateToken(resetPasswordDTO.Token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var userIdClaim = claimsPrincipal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name);
                if (userIdClaim == null)
                {
                    return BadRequest(new { message = "無效的令牌" });
                }

                var userId = int.Parse(userIdClaim.Value);
                var userAccount = _context.UserAccounts.FirstOrDefault(u => u.UserId == userId);
                if (userAccount == null)
                {
                    return BadRequest(new { message = "無效的令牌" });
                }

                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(resetPasswordDTO.NewPassword);
                userAccount.Password = hashedPassword;
                _context.SaveChanges();

                return Ok(new { message = "密碼已成功重設" });
            }
            catch
            {
                return BadRequest(new { message = "無效的令牌" });
            }
        }
    }
}
