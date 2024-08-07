//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using BabyCiao.Models;

//namespace BabyCiao.Controllers
//{
//    public class UserInformationsController : Controller
//    {
//        private readonly BabyciaoContext _context;

//        public UserInformationsController(BabyciaoContext context)
//        {
//            _context = context;
//        }
//        private static readonly Dictionary<int, string> GenderDictionary = new Dictionary<int, string>
//          {
//            { 1, "男" },
//            { 2, "女" },
//           };

//        // GET: UserInformations

//        public async Task<IActionResult> Index()
//        {
//            var babyCiaoContext = _context.UserInformation.Include(u => u.AccountUserNavigation);
//            ViewBag.GenderDictionary = GenderDictionary;
//            return View(await babyCiaoContext.ToListAsync());
//        }

//        // GET: UserInformations/Details/5
//        [HttpGet]
//        public async Task<IActionResult> Details(int UserinfoID)
//        {
//            if (UserinfoID == null)
//            {
//                return NotFound();
//            }

//            var userInformation = await _context.UserInformation
//                .Include(u => u.AccountUserNavigation)
//                .FirstOrDefaultAsync(m => m.UserinfoId == UserinfoID);
//            if (userInformation == null)
//            {
//                return NotFound();
//            }

//            return View(userInformation);
//        }

//        // GET: UserInformations/Create
//        [HttpGet]
//        public IActionResult Create()
//        {
//            ViewData["AccountUser"] = new SelectList(_context.UserAccounts, "Account", "Account");
//            return View();
//        }

//        // POST: UserInformations/Create
//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create([Bind("UserinfoId,AccountUser,UserFirstName,UserLastName,Phone,Address,Gender,Email,Birthday")] UserInformation userInformation)
//        {
//            if (ModelState.IsValid)
//            {
//                _context.Add(userInformation);
//                await _context.SaveChangesAsync();
//                return RedirectToAction(nameof(Index));
//            }
//            ViewData["AccountUser"] = new SelectList(_context.UserAccounts, "Account", "Account", userInformation.AccountUser);
//            return View(userInformation);
//        }

//        // GET: UserInformations/Edit/5
//        [HttpGet]
//        public async Task<IActionResult> Edit(int UserinfoID)
//        {
//            if (UserinfoID == null)
//            {
//                return NotFound();
//            }

//            var userInformation = await _context.UserInformation.FindAsync(UserinfoID);
//            if (userInformation == null)
//            {
//                return NotFound();
//            }
//            ViewData["AccountUser"] = new SelectList(_context.UserAccounts, "Account", "Account", userInformation.AccountUser);
//            return View(userInformation);
//        }

//        // POST: UserInformations/Edit/5
//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        //[Route("/UserInformations/EditPost/{UserinfoID?}")]
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> EditPost(int? UserinfoID, [Bind("UserinfoId,AccountUser,UserFirstName,UserLastName,Phone,Address,Gender,Email,Birthday")] UserInformation userInformation)
//        {
//            if (UserinfoID != userInformation.UserinfoId)
//            {
//                return NotFound();
//            }

//                try
//                {
//                    _context.Update(userInformation);
//                    await _context.SaveChangesAsync();
//                }
//                catch (DbUpdateConcurrencyException)
//                {
//                    if (!UserInformationExists(userInformation.UserinfoId))
//                    {
//                        return NotFound();
//                    }
//                    else
//                    {
//                        throw;
//                    }
//                }
//                return RedirectToAction("Index");

//            ViewData["AccountUser"] = new SelectList(_context.UserAccounts, "Account", "Account", userInformation.AccountUser);
//            return View(userInformation);
//        }

//        // GET: UserInformations/Delete/5
//        public async Task<IActionResult> Delete(int UserinfoID)
//        {
//            if (UserinfoID == null)
//            {
//                return NotFound();
//            }

//            var userInformation = await _context.UserInformation
//                .Include(u => u.AccountUserNavigation)
//                .FirstOrDefaultAsync(m => m.UserinfoId == UserinfoID);
//            if (userInformation == null)
//            {
//                return NotFound();
//            }

//            return View(userInformation);
//        }

//        // POST: UserInformations/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteConfirmed(int UserinfoID)
//        {
//            var userInformation = await _context.UserInformation.FindAsync(UserinfoID);
//            if (userInformation != null)
//            {
//                _context.UserInformation.Remove(userInformation);
//            }

//            await _context.SaveChangesAsync();
//            return RedirectToAction(nameof(Index));
//        }

//        private bool UserInformationExists(int UserinfoID)
//        {
//            return _context.UserInformation.Any(e => e.UserinfoId == UserinfoID);
//        }
//    }
//}