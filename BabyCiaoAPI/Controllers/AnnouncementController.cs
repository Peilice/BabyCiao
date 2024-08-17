using BabyCiaoAPI.DTO;
using BabyCiaoAPI.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BabyCiaoAPI.Controllers
{
    [EnableCors("andy")]
    [Route("api/[controller]")]
    [ApiController]
    public class AnnouncementController : ControllerBase
    {
        private readonly BabyciaoContext _context;
        public AnnouncementController(BabyciaoContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<getAnnouncement_DTO>>> getAnnouncements()
        {
            var s = _context.Announcements.Where(a=>a.Display==true).ToList();
            
            List<getAnnouncement_DTO> DTOs= new List<getAnnouncement_DTO>();
            foreach (var item in s) {
                getAnnouncement_DTO dTO = new getAnnouncement_DTO();
                dTO.id = item.Id;
                dTO.Tittle = item.Tittle;
                dTO.PublishTime = item.PublishTime;
                dTO.Type = item.Type;
                DTOs.Add(dTO);
            }

            return DTOs;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<getAnnouncement_once_DTO>> getAnnouncement_once(int id)
        {
            var s = _context.Announcements.Where(c => c.Id == id).FirstOrDefault();
            getAnnouncement_once_DTO DTO = new getAnnouncement_once_DTO();
            
            DTO.Tittle = s.Tittle;
            DTO.PublishTime = s.PublishTime;
            DTO.Article = s.Article;
            DTO.ReferenceName = s.ReferenceName;
            DTO.ReferenceRoute = s.ReferenceRoute;
            DTO.Type = s.Type;
            
            return DTO;
        }
    }
}
