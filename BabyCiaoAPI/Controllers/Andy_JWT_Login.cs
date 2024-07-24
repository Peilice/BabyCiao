using BCrypt.Net;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BabyCiaoAPI.Models;
using BabyCiaoAPI.DTO;

namespace BabyCiaoAPI.Controllers
{
    [EnableCors("andy")]
    [Route("api/[controller]")]
    [ApiController]
    public class Andy_JWT_Login : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly BabyciaoContext _context;
        private readonly IHttpContextAccessor _httpcontextAccessor;

        public Andy_JWT_Login(IConfiguration configuration, BabyciaoContext context, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _context = context;
            _httpcontextAccessor = httpContextAccessor;
        }
        //[Authorize(Roles = "電子聯絡簿")]
        [HttpGet]
        public async Task<ActionResult<string>> GetUserName()
        {
            var user = _httpcontextAccessor.HttpContext.User.FindFirstValue(JwtRegisteredClaimNames.Name);


            return user;
        }


        //post:api/Home/
        //建立token
        [HttpPost]
        public async Task<ActionResult<string>> CreateToken([FromBody] User my_account)
        {
            var accounts = _context.UserAccounts.Where(m => m.Account == my_account.name).FirstOrDefault();

            bool check = BCrypt.Net.BCrypt.EnhancedVerify(my_account.password, accounts.Password);
            List<string> user_roles = getKeysByAccountName(accounts.Account);
            var varClaims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Name,accounts.Account),

                };
            foreach (string role in user_roles)
            {
                varClaims.Add(new Claim(ClaimTypes.Role, role));
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var jwt = new JwtSecurityToken(
                claims: varClaims,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: creds
                );
            var token = new JwtSecurityTokenHandler().WriteToken(jwt);
            return token;
        }
        List<string> getKeysByAccountName(string name)
        {
            //獲得登錄者的權限編號
            var user = _context.UserAccounts.Where(u => u.Account == name).FirstOrDefault();
            var user_Permission = user.Permissions;

            //將權限群組及功能項目等2項table合併(join)
            var authGroup = _context.FunctionSettings.Join(_context.SystemFunctions, f => f.FunctionCodeSystemFunction, s => s.FunctionId, (f, s) => new
            {
                權限群組 = f.GroupIdAuthGroup,
                功能名稱 = s.FunctionName
            });
            //藉由權限編號，獲得登錄者的功能細項
            var keys = authGroup.Where(k => k.權限群組 == user_Permission).ToList();

            //將登錄者的功能細項(string)加到list中
            List<string> keysOfFunction = new List<string>();
            foreach (var key in keys)
            {
                keysOfFunction.Add(key.功能名稱);
            }

            return keysOfFunction;
        }
        

    }
}
