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
            // 抓取會員數的總筆數
            var parentTotalCount = await _context.UserAccounts.CountAsync();

            // 抓取聯絡簿的總筆數
            var nannyTotalCount = await _context.ContactBooks.CountAsync();

            // 定義需要顯示的縣市列表
            var cities = new List<string>
            {
                "台北市", "新北市", "桃園市", "台中市", "台南市", "高雄市",
                "新竹縣", "新竹市", "苗栗縣", "彰化縣", "南投縣", "雲林縣",
                "嘉義縣", "嘉義市", "屏東縣", "宜蘭縣", "花蓮縣", "台東縣"
            };

            // 抓取全台各縣市會員的總筆數
            var addressTotalCount = await _context.UserInformations
                .Where(u => cities.Any(city => u.Address.StartsWith(city)))
                .GroupBy(u => cities.FirstOrDefault(city => u.Address.StartsWith(city)))
                .Select(g => new AddressCountViewModel
                {
                    Address = g.Key,
                    Count = g.Count()
                })
                .ToListAsync();

            // 確保所有縣市都有數據
            var addressTotalCountFinal = cities
                .Select(city => new AddressCountViewModel
                {
                    Address = city,
                    Count = addressTotalCount.FirstOrDefault(a => a.Address == city)?.Count ?? 0
                })
                .ToList();

            // 抓取各競賽名稱的總筆數
            var competitionNameCount = await _context.OnlineCompetitions
                .GroupBy(c => c.CompetitionName)
                .Select(g => new CompetitionNameCountViewModel
                {
                    CompetitionName = g.Key,
                    Count = g.Count()
                })
                .ToListAsync();

            // 準備數據發送到視圖
            var model = new DataAnalysisViewModel
            {
                ParentTotalCount = parentTotalCount,
                NannyTotalCount = nannyTotalCount,
                AddressTotalCount = addressTotalCountFinal,
                CompetitionNameCount = competitionNameCount
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
        public string CompetitionName { get; set; }
        public int Count { get; set; }
    }

    public class DataAnalysisViewModel
    {
        public int ParentTotalCount { get; set; }
        public int NannyTotalCount { get; set; }
        public List<AddressCountViewModel> AddressTotalCount { get; set; }
        public List<CompetitionNameCountViewModel> CompetitionNameCount { get; set; }
    }
}
