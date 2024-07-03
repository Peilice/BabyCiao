using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BabyCiao.Models;
using BabyCiao.Models.DTO;
using Microsoft.AspNetCore.Hosting;

namespace BabyCiao.Controllers
{
    public class SecondHandController : Controller
    {
        private readonly BabyCiaoContext _context;

        public SecondHandController(BabyCiaoContext context)
        {
            _context = context;
        }

		public async Task<IActionResult> Index()
		{
			var sec = from sc in _context.SecondHandSupplies
					  select new SecondHandDTO
					  {
						  Id = sc.Id,
						  AccountUserAccount = sc.AccountUserAccount,
						  SuppliesName = sc.SuppliesName,
						  StockQuantity = sc.StockQuantity,
						  Type = sc.Type,
						  Display = sc.Display,
						  DisplayString = sc.Display ? "☑" : "",
						  ModifiedTime = sc.ModifiedTime,
					  };
			return View(sec);

		}
		// GET: SecondHand/IndexJson
		public async Task<IActionResult> IndexJson()
		//public async Task<JsonResult> IndexJson()
		{
			var sec = from sc in _context.SecondHandSupplies
					  select new SecondHandDTO
					  {
						  Id = sc.Id,
						  AccountUserAccount = sc.AccountUserAccount,
						  SuppliesName = sc.SuppliesName,
						  StockQuantity = sc.StockQuantity,
						  Type = sc.Type,
						  Display = sc.Display,
						  DisplayString = sc.Display ? "☑" : "",
                          ModifiedTime = sc.ModifiedTime,
                          ModifiedTimeString= sc.ModifiedTime.ToString("yyyy-MM-dd")
                      };
			return Json(sec);
		}

		//// GET: SecondHand/Details/5
		//public async Task<IActionResult> Details(int? id)
  //      {
  //          if (id == null)
  //          {
  //              return NotFound();
  //          }

  //          var secondHandSupplies = await _context.SecondHandSupplies
  //              .FirstOrDefaultAsync(m => m.Id == id);
  //          if (secondHandSupplies == null)
  //          {
  //              return NotFound();
  //          }

  //          return View(secondHandSupplies);
  //      }

  //      // GET: SecondHand/Create
  //      public IActionResult Create()
  //      {
  //          return View();
  //      }

  //      // POST: SecondHand/Create
  //      // To protect from overposting attacks, enable the specific properties you want to bind to.
  //      // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
  //      [HttpPost]
  //      [ValidateAntiForgeryToken]
  //      public async Task<IActionResult> Create([Bind("Id,AccountUserAccount,SuppliesName,SuppliesDescription,StockQuantity,ModifiedTime,Type,Display")] SecondHandSupplies secondHandSupplies)
  //      {
  //          if (ModelState.IsValid)
  //          {
  //              _context.Add(secondHandSupplies);
  //              await _context.SaveChangesAsync();
  //              return RedirectToAction(nameof(Index));
  //          }
  //          return View(secondHandSupplies);
  //      }

        // GET: SecondHand/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var sec = ( from sc in _context.SecondHandSupplies
                        where sc.Id == id
                      select new SecondHandDTO
                      {
                          Id = sc.Id,
                          AccountUserAccount = sc.AccountUserAccount,
                          SuppliesName = sc.SuppliesName,
                          StockQuantity = sc.StockQuantity,
                          Type = sc.Type,
                          Display = sc.Display,
                          DisplayString = sc.Display ? "☑" : "",
                          ModifiedTime = sc.ModifiedTime,
                      }).FirstOrDefault();
            if (sec == null)
            {
                return NotFound();
            }
            return View(sec);
        }

        // POST: SecondHand/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SecondHandDTO sec)
        {
            if (id == null)
            {
                return NotFound();
            }


            var pp = await _context.SecondHandSupplies.FindAsync(id);
            if (pp == null)
            {
                return NotFound();
            }

            // 更新實例的屬性
            pp.Display = sec.Display;

                await _context.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));


        }

        // GET: SecondHand/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var secondHandSupplies = await _context.SecondHandSupplies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (secondHandSupplies == null)
            {
                return NotFound();
            }

            return View(secondHandSupplies);
        }

        // POST: SecondHand/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var secondHandSupplies = await _context.SecondHandSupplies.FindAsync(id);
            if (secondHandSupplies != null)
            {
                _context.SecondHandSupplies.Remove(secondHandSupplies);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SecondHandSuppliesExists(int id)
        {
            return _context.SecondHandSupplies.Any(e => e.Id == id);
        }
    }
}
