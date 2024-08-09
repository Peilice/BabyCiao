using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BabyCiaoAPI.Models;

namespace BabyCiaoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractsController : ControllerBase
    {
        private readonly BabyciaoContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ContractsController(BabyciaoContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: api/Contracts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContractDTO>>> GetContracts()
        {
            return await _context.Contracts
                .Select(contract => new ContractDTO
                {
                    ContractId = contract.ContractId,
                    NannyAccountUserAccount = contract.NannyAccountUserAccount,
                    NannySignature = contract.NannySignature,
                    NannySignatureFile = contract.NannySignatureFile,
                    AccountUserAccount = contract.AccountUserAccount,
                    UserSignature = contract.UserSignature,
                    UserSignatureFile = contract.UserSignatureFile,
                    ContractStartTime = contract.ContractStartTime,
                    ContractFinishTime = contract.ContractFinishTime,
                    ContractFile = contract.ContractFile,
                    Statement = contract.Statement,
                    ModifiedTime = contract.ModifiedTime,
                    BuiledTime = contract.BuiledTime,
                    Display = contract.Display,
                }).ToListAsync();
        }

        // GET: api/Contracts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ContractDTO>> GetContract(int id)
        {
            var contract = await _context.Contracts
                .Select(c => new ContractDTO
                {
                    ContractId = c.ContractId,
                    NannyAccountUserAccount = c.NannyAccountUserAccount,
                    NannySignature = c.NannySignature,
                    NannySignatureFile = c.NannySignatureFile,
                    AccountUserAccount = c.AccountUserAccount,
                    UserSignature = c.UserSignature,
                    UserSignatureFile = c.UserSignatureFile,
                    ContractStartTime = c.ContractStartTime,
                    ContractFinishTime = c.ContractFinishTime,
                    ContractFile = c.ContractFile,
                    Statement = c.Statement,
                    ModifiedTime = c.ModifiedTime,
                    BuiledTime = c.BuiledTime,
                    Display = c.Display,
                }).FirstOrDefaultAsync(c => c.ContractId == id);

            if (contract == null)
            {
                return NotFound();
            }

            return contract;
        }

        // POST: api/Contracts
        [HttpPost]
        public async Task<ActionResult<ContractDTO>> PostContract([FromBody] ContractDTO contractDTO)
        {
            var contract = new Contract
            {
                NannyAccountUserAccount = contractDTO.NannyAccountUserAccount,
                NannySignature = contractDTO.NannySignature,
                NannySignatureFile = contractDTO.NannySignatureFile,
                AccountUserAccount = contractDTO.AccountUserAccount,
                UserSignature = contractDTO.UserSignature,
                UserSignatureFile = contractDTO.UserSignatureFile,
                ContractStartTime = contractDTO.ContractStartTime,
                ContractFinishTime = contractDTO.ContractFinishTime,
                ContractFile = contractDTO.ContractFile,
                Statement = contractDTO.Statement,
                ModifiedTime = DateTime.Now,
                BuiledTime = DateTime.Now,
                Display = contractDTO.Display
            };

            _context.Contracts.Add(contract);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetContract), new { id = contract.ContractId }, contractDTO);
        }

        // PUT: api/Contracts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContract(int id, [FromBody] ContractDTO contractDTO)
        {
            if (id != contractDTO.ContractId)
            {
                return BadRequest();
            }

            var contract = await _context.Contracts.FindAsync(id);
            if (contract == null)
            {
                return NotFound();
            }

            contract.NannyAccountUserAccount = contractDTO.NannyAccountUserAccount;
            contract.NannySignature = contractDTO.NannySignature;
            contract.NannySignatureFile = contractDTO.NannySignatureFile;
            contract.AccountUserAccount = contractDTO.AccountUserAccount;
            contract.UserSignature = contractDTO.UserSignature;
            contract.UserSignatureFile = contractDTO.UserSignatureFile;
            contract.ContractStartTime = contractDTO.ContractStartTime;
            contract.ContractFinishTime = contractDTO.ContractFinishTime;
            contract.ContractFile = contractDTO.ContractFile;
            contract.Statement = contractDTO.Statement;
            contract.ModifiedTime = DateTime.Now;
            contract.Display = contractDTO.Display;

            _context.Entry(contract).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Contracts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContract(int id)
        {
            var contract = await _context.Contracts.FindAsync(id);
            if (contract == null)
            {
                return NotFound();
            }

            _context.Contracts.Remove(contract);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
