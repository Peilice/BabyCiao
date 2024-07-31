using BabyCiaoAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using BabyCiaoAPI.DTO;
using Microsoft.AspNetCore.Cors;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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

        // GET api/OnlineCompetitions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<CompetitionDetailDTO>>> voteInfo(int id)
        {
            var Competitiondetail = await (from com in _context.OnlineCompetitions
                                           join comd in _context.CompetitionDetails
                                           on com.Id equals comd.IdOnlineCompetition
                                           where comd.IdOnlineCompetition== id
                                           select new CompetitionDetailDTO
                                           {
                                               Id= com.Id,
                                               CompetitionName=com.CompetitionName,
                                               StartTime=com.StartTime,
                                               EndTime=com.EndTime,
                                               Content=com.Content,
                                               Statement = com.Statement,
                                               AccountUserAccount = comd.AccountUserAccount,
                                               CompetitionPhotos=comd.CompetitionPhoto,
                                               CompetitionDetailId=comd.IdOnlineCompetition,
                                           }).ToListAsync();
            return Ok(Competitiondetail);
        }

        //GET api/OnlineCompetitions/getRecord
        [HttpGet("getRecord")]
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


        // POST api/<OnlineCompetitionsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
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
