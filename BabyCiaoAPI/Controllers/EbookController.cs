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
    public class EbookController : ControllerBase
    {
        private readonly BabyciaoContext _context;
        private readonly IHttpContextAccessor _contextAccessor;
        //private readonly ContactBook _contactBook;

        public EbookController (BabyciaoContext context, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            //_contactBook = contactBook;
            _contextAccessor= contextAccessor;
        }

        [HttpGet("GetUserName_Ebook")]
        public async Task<ActionResult<string>> GetUserName_Ebook()
        {
            var username = _contextAccessor.HttpContext.User.FindFirstValue(JwtRegisteredClaimNames.Name);
            if (username != null)
            {
                return  username;
            }
            return null;
        }

        // GET: api/<EbookController>
        [HttpGet("GetEBooks")]
        public async Task<IEnumerable<EBook_DTO>> GetEBooks()
        {
            string username = _contextAccessor.HttpContext.User.FindFirstValue(JwtRegisteredClaimNames.Name);
            
            bool check = _context.ContactBooks.Where(c => c.ParentsIdUserAccount == username).Any();
            
            if (check)
            {
                var b = _context.ContactBooks.Where(c => c.ParentsIdUserAccount == username).Select(ebook => new EBook_DTO
                {
                    Id = ebook.Id,
                    ParentsIdUserAccount = ebook.ParentsIdUserAccount,
                    BabyName = ebook.BabyName,
                    Gender = ebook.Gender,
                    Birthday = ebook.Birthday,
                });
                return b;
            }
            else
            {
                return null;
            }
        }
        [HttpPost("createEbook")]
        public async Task<string> createEbook([FromBody] EBook_create_DTO DTO)
        {
            ContactBook ebook = new ContactBook()
            {
                ParentsIdUserAccount = DTO.ParentsIdUserAccount,
                BabyName=DTO.BabyName,
                Gender = DTO.Gender,
                Birthday = DTO.Birthday,
                BloodType = DTO.BloodType,
                EmergencyContact = DTO.EmergencyContact,
                EmergencyContactPhone1 = DTO.EmergencyContactPhone1,
            };
            try
            {
                _context.ContactBooks.Add(ebook);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex) { 
            return ex.ToString();
            }
            return "Ok";
        }
    }
}
