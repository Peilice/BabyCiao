using BabyCiaoAPI.DTO;
using BabyCiaoAPI.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace BabyCiaoAPI.Controllers
{
    [EnableCors("andy")]
    [Route("api/[controller]")]
    [ApiController]
    public class Andy_NannyMatchController : ControllerBase
    {
        private readonly BabyciaoContext _context;
        private readonly IHttpContextAccessor _httpcontextAccessor;
        public Andy_NannyMatchController(BabyciaoContext context, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _httpcontextAccessor = contextAccessor;
        }

        [HttpPost("GetNanny")]
        public async Task<ActionResult<IEnumerable<andy_GetNanny_DTO>>> GetNanny(andy_GetNanny_DTO DTO)
        {

            var s = _context.UserInformations.Join(_context.NannyRequirments.Where(n => n.Statement == 2 && n.AddressesOfAgencies.Contains(DTO.AddressesOfAgencies)), u => u.AccountUser, n => n.NannyAccountUserAccount, (u, n) => new andy_GetNanny_DTO
            {
                Id = n.Id,
                NannyAccountUserAccount = n.NannyAccountUserAccount,
                AddressesOfAgencies = n.AddressesOfAgencies,
                name = u.UserLastName,
            }).ToList();

            return s;
        }
        [HttpGet("CheckNanny")]
        public async Task<ActionResult<bool>> CheckNanny()
        {
            var user = _httpcontextAccessor.HttpContext.User.FindFirstValue(JwtRegisteredClaimNames.Name);

            if (user == null)
            {
                return false;
            }

            bool s =_context.NannyRequirments.Where(n=>n.NannyAccountUserAccount== user && n.Statement==2).Any();
            if (s) { 
                return true;
            }
            else
            {
                return false;
            }

        }
        [HttpGet("CheckParent/{id}")]
        public async Task<ActionResult<bool>> CheckParent(int id)
        {
            var user = _httpcontextAccessor.HttpContext.User.FindFirstValue(JwtRegisteredClaimNames.Name);

            if (user == null)
            {
                return false;
            }

            bool s = _context.ContactBooks.Where(n => n.ParentsIdUserAccount == user && n.Id == id).Any();
            if (s)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        [HttpGet("getContract")]
        public async Task<ActionResult<List<andy_getContract_DTO>>> getContract()
        {
            var user = _httpcontextAccessor.HttpContext.User.FindFirstValue(JwtRegisteredClaimNames.Name);

            if (user == null)
            {
                return null;
            }

            var sss = _context.Contracts.Where(c => c.NannyAccountUserAccount == user).ToList();
            List< andy_getContract_DTO > DTOs=new List<andy_getContract_DTO>();
            if (sss.Any()) 
            {
                foreach (var ss in sss) {
                    int ebookID =Convert.ToInt32(ss.ContractFile);
                    var s = _context.ContactBooks.Where(b=>b.Id== ebookID).FirstOrDefault();
                    andy_getContract_DTO DTO = new andy_getContract_DTO();
                    DTO.ContactBooktId = s.Id;
                    DTO.NannyAccountUserAccount = ss.NannyAccountUserAccount;
                    DTO.AccountUserAccount = s.ParentsIdUserAccount;
                    DTO.BabyName = s.BabyName;
                    DTOs.Add( DTO );
                }
            }
            return DTOs;
        }
        [HttpGet("getContractByParent")]
        public async Task<ActionResult<List<andy_getContract_DTO>>> getContractByParent()
        {
            var user = _httpcontextAccessor.HttpContext.User.FindFirstValue(JwtRegisteredClaimNames.Name);

            if (user == null)
            {
                return null;
            }

            var sss = _context.Contracts.Where(c => c.AccountUserAccount == user).ToList();
            List<andy_getContract_DTO> DTOs = new List<andy_getContract_DTO>();
            if (sss.Any())
            {
                foreach (var ss in sss)
                {
                    int ebookID = Convert.ToInt32(ss.ContractFile);
                    var s = _context.ContactBooks.Where(b => b.Id == ebookID).FirstOrDefault();
                    andy_getContract_DTO DTO = new andy_getContract_DTO();
                    DTO.ContactBooktId = s.Id;
                    DTO.ContractId = ss.ContractId;
                    DTO.NannyAccountUserAccount = ss.NannyAccountUserAccount;
                    DTO.AccountUserAccount = s.ParentsIdUserAccount;
                    DTO.BabyName = s.BabyName;
                    DTOs.Add(DTO);
                }
            }
            return DTOs;
        }
        [HttpPost("MatchNanny")]
        public async Task<ActionResult<string>> MatchNanny(andy_MatchNanny_DTO DTO)
        {
            var user = _httpcontextAccessor.HttpContext.User.FindFirstValue(JwtRegisteredClaimNames.Name);
            if (user == null)
            {
                return null;
            }
            bool check =_context.Contracts.Where(c=>c.NannyAccountUserAccount==DTO.NannyAccountUserAccount && c.AccountUserAccount== user).Any();

            if (check) {
                return "已授權，無須再授權";
            }
            
            Contract contract =new Contract();
            contract.NannyAccountUserAccount=DTO.NannyAccountUserAccount;
            contract.AccountUserAccount= user;
            contract.ContractFile=DTO.ContactBooktId.ToString();

            contract.NannySignatureFile = "noData";
            contract.UserSignatureFile = "noData";
            contract.ContractStartTime = DateOnly.FromDateTime(DateTime.Now);
            contract.ContractFinishTime = DateOnly.FromDateTime(DateTime.Now);
            contract.Statement = "已授權";

            _context.Contracts.Add(contract);
            await _context.SaveChangesAsync();
            return "授權成功";
        }
        [HttpDelete("deleteContractByContractID/{id}")]
        public async Task<ActionResult<string>> deleteContractByContractID(int id)
        {
            var user = _httpcontextAccessor.HttpContext.User.FindFirstValue(JwtRegisteredClaimNames.Name);

            if (user == null)
            {
                return null;
            }
            var s = _context.Contracts.Find(id);
            _context.Contracts.Remove(s);
            await _context.SaveChangesAsync();


            return "刪除成功";
        }
        [HttpGet("getPickUpDatas/{id}")]
        public async Task<ActionResult<List<andy_PickUpDatas_DTO>>> getPickUpDatas(int id)
        {
            var s = _context.Diaries.Where(d => d.IdContactBook==id).OrderByDescending(d=>d.ModifiedTime).ToList();

            List<andy_PickUpDatas_DTO> DTOs = new List<andy_PickUpDatas_DTO>();

            foreach (var d in s)
            {
                andy_PickUpDatas_DTO DTO =new andy_PickUpDatas_DTO();
                DTO.AccountUserAccount = d.AccountUserAccount;
                DTO.ModifiedTime = d.ModifiedTime;
                DTO.Memo = d.Memo;
                DTOs.Add(DTO);
            }
            return DTOs;

        }
        [HttpPost("CreatePickUpDatas")]
        public async Task<ActionResult<string>> CreatePickUpDatas(andy_PickUpDatas_DTO DTO)
        {
            Diary diary = new Diary();
            diary.ModifiedTime = DTO.ModifiedTime;
            diary.Memo = DTO.Memo;
            diary.AccountUserAccount = DTO.AccountUserAccount;
            diary.IdContactBook = DTO.IdContactBook;
            diary.Photo = "NoImage";

            _context.Diaries.Add(diary);
            await _context.SaveChangesAsync();
            return "紀錄成功";

        }
    }
}
