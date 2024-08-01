using BabyCiaoAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using BabyCiaoAPI.DTO;
using Microsoft.AspNetCore.Cors;
using System;
using Microsoft.JSInterop.Infrastructure;


namespace BabyCiaoAPI.Controllers
{
    [EnableCors("andy")]
    [Route("api/[controller]")]
    [ApiController]
    public class OnlineCompetitionsController : ControllerBase
    {
        private readonly BabyciaoContext _context;

        public OnlineCompetitionsController(BabyciaoContext context)
        {
            _context = context;
        }

        // GET: api/OnlineCompetitions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OnlineCompetitionsDTO>>> Get()
        {
            var Competition = await (from com in _context.OnlineCompetitions
                                     join comp in _context.CompetitionPhotos
                                     on com.Id equals comp.IdOnlineCompetition
                                     select new OnlineCompetitionsDTO
                                     {
                                         Id = com.Id,
                                         CompetitionName = com.CompetitionName,
                                         Content = com.Content,
                                         StartTime = com.StartTime,
                                         EndTime = com.EndTime,
                                         Statement = com.Statement,
                                         CompetitionPhotoNames = comp.PhotoName,
                                     }).ToListAsync();
            return Ok(Competition);
        }

        // GET api/OnlineCompetitions/{id}
        [HttpGet("{id}")]
        //public async Task<ActionResult<IEnumerable<CompetitionDetailDTO>>> voteInfo(int id)
        //設定回傳物件是DTO陣列(list)
        public async Task<ActionResult<List<CompetitionDetailDTO>>> voteInfo(int id)
        {
            if(id == null)
            {
                return NotFound();
            }

            //創建要回傳的DTO陣列(list)
            List<CompetitionDetailDTO> competitionDetailDTOs = new List<CompetitionDetailDTO>();

            //利用request的id找尋資料庫內的選手資料，並轉list
            var a = await (from com in _context.OnlineCompetitions
                                           join comd in _context.CompetitionDetails
                                           on com.Id equals comd.IdOnlineCompetition
                                           where comd.IdOnlineCompetition == id
                                           select new CompetitionDetailDTO
                                           {
                                               Id = com.Id,
                                               CompetitionName = com.CompetitionName,
                                               StartTime = com.StartTime,
                                               EndTime = com.EndTime,
                                               Content = com.Content,
                                               Statement = com.Statement,
                                               AccountUserAccount = comd.AccountUserAccount,
                                               CompetitionPhotos = comd.CompetitionPhoto,
                                               CompetitionDetailId = comd.Id,
                                           }).ToListAsync();



            //var a = _context.CompetitionDetails.Where(c => c.IdOnlineCompetition == id).Select(c => new
            //{
            //    AccountUserAccount = c.AccountUserAccount,
            //    CompetitionPhotos = c.CompetitionPhoto,
            //    CompetitionDetailId = c.Id,
            //}).ToList();
            
            List<int> ids = new List<int>();
            List<int> nums = new List<int>();
            //找出選手資料的id，並將結果儲存到 List<int> ids裡面
            foreach (var c in a) {
                ids.Add(c.CompetitionDetailId);
            }
            //再利用ids遍歷得票數***(
            //var num = _context.CompetitionRecords.Where(c2 => c2.IdCompetitionDetail== ???不能是var).Count();
            //)***
            foreach ( var item in ids)
            {
                var num = _context.CompetitionRecords.Where(c2 => c2.IdCompetitionDetail== item).Count();
                nums.Add(num);

            }
            //將選手資料及得票數包進DTO list內
            for (int i = 0; i < nums.Count(); i++)
            {
                CompetitionDetailDTO dto = new CompetitionDetailDTO();
                dto.AccountUserAccount= a[i].AccountUserAccount;
                dto.CompetitionPhotos= a[i].CompetitionPhotos;
                dto.CompetitionDetailId= a[i].CompetitionDetailId;
                dto.CompetitionName = a[i].CompetitionName;
                dto.Content = a[i].Content;
                dto.Statement = a[i].Statement;
                dto.StartTime = a[i].StartTime;
                dto.EndTime = a[i].EndTime;
                dto.number = nums[i];

                competitionDetailDTOs.Add(dto);

            }

            return Ok(competitionDetailDTOs);
        }

        //GET api/OnlineCompetitions/getRecord
        [HttpGet("getRecord/{id}")]
        public async Task<ActionResult<IEnumerable<CompetitionDetailDTO>>> getRecord(int id)
        {
            var RecordDetail = await (from com in _context.OnlineCompetitions
                                           join comd in _context.CompetitionDetails
                                           on com.Id equals comd.IdOnlineCompetition
                                           join comr in _context.CompetitionRecords
                                           on comd.Id equals comr.IdCompetitionDetail
                                           where comr.IdCompetitionDetail == id
                                           select new CompetitionDetailDTO
                                           {
                                               Id = com.Id,
                                               CompetitionName = com.CompetitionName,
                                               StartTime = com.StartTime,
                                               EndTime = com.EndTime,
                                               Content = com.Content,
                                               Statement = com.Statement,
                                               AccountUserAccount = comd.AccountUserAccount,
                                               CompetitionPhotos = comd.CompetitionPhoto,
                                               CompetitionDetailId = comd.IdOnlineCompetition,
                                           }).ToListAsync();
            return Ok(RecordDetail);

        }


        // POST api/OnlineCompetitions/apply
        // 報名比賽
        [HttpPost("apply")]
        public async Task<string> apply([FromBody] CompetitionDetailDTO comDetailDTO)
        {
            CompetitionDetail applyfor = new CompetitionDetail()
            {
                IdOnlineCompetition=comDetailDTO.Id,
                AccountUserAccount = comDetailDTO.AccountUserAccount,
                CompetitionPhoto=comDetailDTO.CompetitionPhotos,
                Content = comDetailDTO.Content,
            };
            try
            {
                _context.Add(applyfor);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex) 
            { 
                return ex.ToString();
            }
            

            return "Ok";

        }

        // PUT api/<OnlineCompetitionsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<OnlineCompetitionsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
