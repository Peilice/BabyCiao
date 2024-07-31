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
            // 抓取家長的總筆數
            var parentTotalCount = await _context.BabyResumes
                .CountAsync();

            // 抓取保母的總筆數
            var nannyTotalCount = await _context.NannyResume
                .CountAsync();

            // 準備數據發送到視圖
            var model = new DataAnalysisViewModel
            {
                ParentTotalCount = parentTotalCount,
                NannyTotalCount = nannyTotalCount
            };

            return View(model);
        }
    }

    public class DataAnalysisViewModel
    {
        public int ParentTotalCount { get; set; }
        public int NannyTotalCount { get; set; }
    }
}
