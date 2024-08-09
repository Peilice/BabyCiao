using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BabyCiao.Models;
using BCrypt.Net;

namespace BabyCiao.Controllers
{
    [Route("/UserAccounts/{action=Index}/{UserID?}")]
    public class UserAccountsController : Controller
    {
        private readonly BabyciaoContext _context;

        public UserAccountsController(BabyciaoContext context)
        {
            _context = context;
        }

        private static readonly Dictionary<int, string> PermissionsDictionary = new Dictionary<int, string>
        {
            { 0, "停權" },
            { 1, "家長" },
            { 2, "保母" },
            { 3, "家長 / 保母" },
            { 4, "客服"},
            { 5, "管理員"},
        };

        // GET: UserAccounts
        [HttpGet]
        public async Task<IActionResult> Index(string selectedPermission = null)
        {
            var userAccounts = await _context.UserAccounts.ToListAsync();

            if (!string.IsNullOrEmpty(selectedPermission))
            {
                userAccounts = userAccounts.Where(u => u.Permissions == int.Parse(selectedPermission)).ToList();
            }

            var permissionsSelectList = PermissionsDictionary.Select(p => new SelectListItem
            {
                Value = p.Key.ToString(),
                Text = p.Value
            }).ToList();

            ViewBag.Permissions = new SelectList(permissionsSelectList, "Value", "Text", selectedPermission);
            ViewBag.UserIds = userAccounts.Select(u => u.UserId).ToList();
            ViewBag.Accounts = userAccounts.Select(u => u.Account).ToList();
            ViewBag.Passwords = userAccounts.Select(u => u.Password).ToList();
            ViewBag.PermissionsList = userAccounts.Select(u => u.Permissions).ToList();
            ViewBag.Vips = userAccounts.Select(u => u.Vip).ToList();
            ViewBag.PermissionsDictionary = PermissionsDictionary;

            return View();
        }

        // GET: UserAccounts/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int UserID)
        {
            if (UserID == null)
            {
                return NotFound();
            }

            var userAccount = await _context.UserAccounts
                .FirstOrDefaultAsync(m => m.UserId == UserID);
            if (userAccount == null)
            {
                return NotFound();
            }

            return View(userAccount);
        }

        // GET: UserAccounts/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserAccounts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost([Bind("UserId,Account,Password,Permissions,Vip")] UserAccount userAccount)
        {
            if (ModelState.IsValid)
            {
                // 對密碼進行加密
                userAccount.Password = EncryptPassword(userAccount.Password);

                _context.Add(userAccount);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userAccount);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int UserID)
        {
            if (UserID == null)
            {
                return NotFound();
            }

            var userAccount = await _context.UserAccounts.FindAsync(UserID);
            if (userAccount == null)
            {
                return NotFound();
            }

            // 確保 ViewBag.PermissionsDictionary 被正確設置
            ViewBag.PermissionsDictionary = PermissionsDictionary;

            return View(userAccount);
        }


        // POST: UserAccounts/Edit/5
        [HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Edit(int UserID, string Account, int Permissions, string? Password)
{
    var userAccount = await _context.UserAccounts.AsNoTracking().FirstOrDefaultAsync(u => u.UserId == UserID);

    if (userAccount == null)
    {
        return NotFound();
    }

    if (ModelState.IsValid)
    {
        try
        {
            userAccount.Permissions = Permissions;
            if (!string.IsNullOrEmpty(Password))
            {
                userAccount.Password = EncryptPassword(Password);
                 
            }
            
            _context.Update(userAccount);
            await _context.SaveChangesAsync();
            
        }
        catch (DbUpdateConcurrencyException ex)
        {
            
            if (!UserAccountExists(userAccount.UserId))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }
        return RedirectToAction(nameof(Index));
    }
    
   
    ViewBag.PermissionsDictionary = PermissionsDictionary; 
    return View(userAccount);
}



        // GET: UserAccounts/Delete/5
        public async Task<IActionResult> Delete(int UserID)
        {
            if (UserID == null)
            {
                return NotFound();
            }

            var userAccount = await _context.UserAccounts
                .FirstOrDefaultAsync(m => m.UserId == UserID);
            if (userAccount == null)
            {
                return NotFound();
            }

            return View(userAccount);
        }

        // POST: UserAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int UserID)
        {
            var userAccount = await _context.UserAccounts.FindAsync(UserID);
            if (userAccount != null)
            {
                _context.UserAccounts.Remove(userAccount);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserAccountExists(int UserID)
        {
            return _context.UserAccounts.Any(e => e.UserId == UserID);
        }

        // 加密密碼的函數
        private string EncryptPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
}
