using Microsoft.AspNetCore.Mvc;
using BabyCiaoAPI.Models;
using BabyCiaoAPI.DTO;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BabyCiaoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EbookController : ControllerBase
    {
        private readonly BabyciaoContext _context;

        private readonly ContactBook _contactBook;

        public EbookController (BabyciaoContext context, ContactBook contactBook)
        {
            _context = context;
            _contactBook = contactBook;
        }


        // GET: api/<EbookController>
        [HttpGet]
        public async Task<EBook_DTO> Get()
        {
            string username = HttpContext.User.Identity.Name;
            int userId = 0;
            var s =_context.UserAccounts.Where(u=>u.Account==username).FirstOrDefault();
            if (s != null)
            {
                userId = s.UserId;
                var b=_context.ContactBooks.Where(c=>c.ParentsIdUserAccount==username).FirstOrDefault();
                if (b != null) {
                    EBook_DTO _DTO = new EBook_DTO()
                    {
                        Id = b.Id,
                        ParentsIdUserAccount = b.ParentsIdUserAccount,
                        BabyName = b.BabyName,
                        Gender = b.Gender,
                        Birthday = b.Birthday,
                    };
                    return _DTO;
                }
                else
                {
                    return null;
                }
            }
            else {
                return null;
            }
        }

        // GET api/<EbookController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            
            
            return "value";
        }

        // POST api/<EbookController>
        [HttpPost]
        public void Post([FromBody] string value)
        {

        }

        // PUT api/<EbookController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<EbookController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
