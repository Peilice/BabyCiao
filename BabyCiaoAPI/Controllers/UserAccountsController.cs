using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using BabyCiaoAPI.DTO;
using BabyCiaoAPI.Models;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using BCrypt.Net;

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
                return BadRequest(new { message = "Account already exists." });
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
            return Ok(new { message = "登入成功", token });
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
    }
}
