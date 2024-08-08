using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BabyCiaoAPI.DTO;
using BabyCiaoAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BabyCiaoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerServiceController : ControllerBase
    {
        private readonly BabyciaoContext _context;

        public CustomerServiceController(BabyciaoContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerServiceDTO>>> GetTickets()
        {
            var tickets = await _context.CustomerServices
                .Select(cs => new CustomerServiceDTO
                {
                    Id = cs.Id,
                    UserName = cs.UserName,
                    Phone = cs.Phone,
                    Email = cs.Email,
                    Title = cs.Title,
                    Context = cs.Context,
                    Type = cs.Type,
                    Statement = cs.Statement,
                    AccountUserAccount = cs.AccountUserAccount,
                    Createddated = cs.Createddated,
                    ModiifiedDate = cs.ModiifiedDate
                })
                .ToListAsync();

            return Ok(tickets);
        }

        [HttpPost]
        public async Task<ActionResult<CustomerServiceDTO>> CreateTicket([FromBody] CustomerServiceDTO newTicketDto)
        {
            var newTicket = new CustomerService
            {
                UserName = newTicketDto.UserName,
                Phone = newTicketDto.Phone,
                Email = newTicketDto.Email,
                Title = newTicketDto.Title,
                Context = newTicketDto.Context,
                Type = newTicketDto.Type,
                Statement = newTicketDto.Statement,
                AccountUserAccount = newTicketDto.AccountUserAccount,
                Createddated = DateTime.Now,
                ModiifiedDate = DateTime.Now
            };

            _context.CustomerServices.Add(newTicket);
            await _context.SaveChangesAsync();

            newTicketDto.Id = newTicket.Id;
            newTicketDto.Createddated = newTicket.Createddated;
            newTicketDto.ModiifiedDate = newTicket.ModiifiedDate;

            return CreatedAtAction(nameof(GetTickets), new { id = newTicketDto.Id }, newTicketDto);
        }
    }
}
