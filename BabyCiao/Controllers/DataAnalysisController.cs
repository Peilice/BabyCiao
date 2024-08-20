using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BabyCiao.Models;
using BabyCiao.ViewModel;
using System.Collections.Generic;

namespace BabyCiao.Controllers
{
    public class DataAnalysisController : Controller
    {
        private readonly BabyciaoContext _context;

        public DataAnalysisController(BabyciaoContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var parentTotalCount = await _context.UserAccounts.CountAsync();
            var nannyTotalCount = await _context.ContactBooks.CountAsync();

            var cities = new List<string>
            {
                "台北市", "新北市", "桃園市", "台中市", "台南市", "高雄市",
                "新竹縣", "新竹市", "苗栗縣", "彰化縣", "南投縣", "雲林縣",
                "嘉義縣", "嘉義市", "屏東縣", "宜蘭縣", "花蓮縣", "台東縣"
            };

            var addressTotalCount = await _context.UserInformations
                .Where(u => cities.Any(city => u.Address.StartsWith(city)))
                .GroupBy(u => cities.FirstOrDefault(city => u.Address.StartsWith(city)))
                .Select(g => new AddressCountViewModel
                {
                    Address = g.Key,
                    Count = g.Count()
                })
                .ToListAsync();

            var addressTotalCountFinal = cities
                .Select(city => new AddressCountViewModel
                {
                    Address = city,
                    Count = addressTotalCount.FirstOrDefault(a => a.Address == city)?.Count ?? 0
                })
                .ToList();

            var competitionNameCount = await _context.OnlineCompetitions
                .Select(c => new CompetitionNameCountViewModel
                {
                    IdOnlineCompetition = c.Id,
                    CompetitionName = c.CompetitionName
                })
                .ToListAsync();

            var competitionRecordCount = await _context.CompetitionRecords
                .GroupBy(cr => cr.IdOnlineCompetition)
                .Select(g => new CompetitionRecordCountViewModel
                {
                    IdOnlineCompetition = g.Key,
                    Count = g.Count()
                })
                .ToListAsync();

            // 先將 CompetitionDetails 和 CompetitionRecords 的數據讀取到內存中
            var competitionDetails = await _context.CompetitionDetails
                .Include(d => d.IdOnlineCompetitionNavigation)
                .ToListAsync();

            var competitionRecords = await _context.CompetitionRecords.ToListAsync();

            // 在內存中進行計算
            var competitionUserVotes = competitionDetails
                .Select(d => new CompetitionUserVotesViewModel
                {
                    CompetitionName = d.IdOnlineCompetitionNavigation.CompetitionName,
                    AccountUserAccount = d.AccountUserAccount,
                    VoteCount = competitionRecords
                        .Where(cr => cr.IdCompetitionDetail == d.Id)
                        .Count()
                })
                .ToList();

            var model = new DataAnalysisViewModel
            {
                ParentTotalCount = parentTotalCount,
                NannyTotalCount = nannyTotalCount,
                AddressTotalCount = addressTotalCountFinal,
                CompetitionNameCount = competitionNameCount,
                CompetitionRecordCount = competitionRecordCount,
                CompetitionUserVotes = competitionUserVotes
            };

            return View(model);
        }
    }

    public class AddressCountViewModel
    {
        public string Address { get; set; }
        public int Count { get; set; }
    }

    public class CompetitionNameCountViewModel
    {
        public int IdOnlineCompetition { get; set; }
        public string CompetitionName { get; set; }
    }

    public class CompetitionRecordCountViewModel
    {
        public int IdOnlineCompetition { get; set; }
        public int Count { get; set; }
    }

    public class CompetitionUserVotesViewModel
    {
        public string CompetitionName { get; set; }
        public string AccountUserAccount { get; set; }
        public int VoteCount { get; set; }
    }

    public class DataAnalysisViewModel
    {
        public int ParentTotalCount { get; set; }
        public int NannyTotalCount { get; set; }
        public List<AddressCountViewModel> AddressTotalCount { get; set; }
        public List<CompetitionNameCountViewModel> CompetitionNameCount { get; set; }
        public List<CompetitionRecordCountViewModel> CompetitionRecordCount { get; set; }
        public List<CompetitionUserVotesViewModel> CompetitionUserVotes { get; set; }
    }
}
