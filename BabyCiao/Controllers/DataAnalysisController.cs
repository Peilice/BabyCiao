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
            var parentTotalCount = await _context.UserAccounts
                .CountAsync();

            // 抓取聯絡簿的總筆數
            var nannyTotalCount = await _context.ContactBooks
                .CountAsync();

            var AddressTotalCount = await _context.UserInformations
               .CountAsync();

            // 準備數據發送到視圖
            var model = new DataAnalysisViewModel
            {
                ParentTotalCount = parentTotalCount,
                NannyTotalCount = nannyTotalCount,
                AddressTotalCount = AddressTotalCount
            };

            return View(model);
        }
    }

    public class DataAnalysisViewModel
    {
        public int ParentTotalCount { get; set; }
        public int NannyTotalCount { get; set; }
        public int AddressTotalCount { get; set; }
    }
}
