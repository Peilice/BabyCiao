using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BabyCiao.Models;
using BabyCiao.Models.DTO;

namespace BabyCiao.Controllers
{
	public class GroupBuyController : Controller
	{
		private readonly BabyCiaoContext _context;
		private IWebHostEnvironment _webHostEnvironment;

		public GroupBuyController(BabyCiaoContext context, IWebHostEnvironment webHostEnvironment)
		{
			_context = context;
			_webHostEnvironment = webHostEnvironment;
		}

		public async Task<IActionResult> Index()
		{
			var groupBuys = from gb in _context.GroupBuyings
							join gbp in _context.GroupBuyingPhotos on gb.Id equals gbp.IdGroupBuying into pp
							from gbp in pp.OrderBy(p => p.PhotoName).Take(1).DefaultIfEmpty()
							select new GroupBuyDTO
							{
								Id = gb.Id,
								UserAccount = gb.AccountUserAccount,
								ProductName = gb.ProductName,
								ProductDescription = gb.ProductDescription,
								TargetCount = gb.TargetCount,
								Price = gb.Price,
								Statement = gb.Statement,
								ModifiedTime = gb.ModifiedTime,
								ModifiedTimeView = gb.ModifiedTime.ToString("yyyy-MM-dd"),
								Display = gb.Display,

								DisplayString = gb.Display ? "☑" : "",
								JoinQuantity = _context.GroupBuyingDetails.Where(id => id.GroupBuyingId == gb.Id).Sum(q => q.Quantity),
								photoUrl = gbp.PhotoName != null ? $"<img src=\" /uploads/{gbp.PhotoName}\" width=\"100\" />" : "<img src=\" /img/noImage.jpg\" width=\"100\" />"

							};
			//return View(await groupBuys.ToListAsync());
			return View(groupBuys);
		}
		// GET: Products/IndexJson
		public async Task<IActionResult> IndexJson()
        //public async Task<JsonResult> IndexJson()
        {
            var groupBuys = from gb in _context.GroupBuyings
                            join gbp in _context.GroupBuyingPhotos on gb.Id equals gbp.IdGroupBuying into pp
                            from gbp in pp.OrderBy(p => p.PhotoName).Take(1).DefaultIfEmpty()
                            select new GroupBuyDTO
                            {
                                Id = gb.Id,
                                UserAccount = gb.AccountUserAccount,
                                ProductName = gb.ProductName,
                                ProductDescription = gb.ProductDescription,
                                TargetCount = gb.TargetCount,
                                Price = gb.Price,
                                Statement = gb.Statement,
                                ModifiedTime = gb.ModifiedTime,
                                ModifiedTimeView = gb.ModifiedTime.ToString("yyyy-MM-dd"),
                                Display = gb.Display,
								DisplayString=gb.Display? "☑":"",

								JoinQuantity = _context.GroupBuyingDetails.Where(id => id.GroupBuyingId == gb.Id).Sum(q => q.Quantity),
                                photoUrl = gbp.PhotoName != null ? $"<img src=\" /uploads/{gbp.PhotoName}\" width=\"100\" />" : "<img src=\" /img/noImage.jpg\" width=\"100\" />"

                            };
            return Json(groupBuys);
		}
		// GET: GroupBuy
		public async Task<IActionResult> OrdersJson()
		//public async Task<JsonResult> IndexJson()
		{

			var orders = from gbd in _context.GroupBuyingDetails
						 join gb in _context.GroupBuyings on gbd.GroupBuyingId equals gb.Id
						 select new GroupBuyDTO
						 {
							 JoinId = gbd.Id,
							 JoinGroupId = gbd.GroupBuyingId,
							 JoinUserAccount = gbd.AccountUserAccount,
							 Quantity = gbd.Quantity,
							 OrderPrice = gbd.Quantity * gb.Price,
							 JoinModifiedTime = gbd.ModifiedTime,
							 ViewJoinModifiedTime = gbd.ModifiedTime.ToString("yyyy-MM-dd"),
							 Statement = gbd.Statement,
							 Price = gb.Price,
						 };
			return Json(orders);
		}
		public async Task<IActionResult> Orders()
		{
			var orders = from gbd in _context.GroupBuyingDetails
						 join gb in _context.GroupBuyings on gbd.GroupBuyingId equals gb.Id 
						 select new GroupBuyDTO
						 {
							 JoinId = gbd.Id,
							 JoinGroupId = gbd.GroupBuyingId,
							 JoinUserAccount = gbd.AccountUserAccount,
							 Quantity = gbd.Quantity,
							 OrderPrice = gbd.Quantity * gb.Price,
							 ViewJoinModifiedTime = gbd.ModifiedTime.ToString("yyyy-MM-dd"),
							 JoinModifiedTime = gbd.ModifiedTime,
							 Statement = gbd.Statement,
							 Price = gb.Price,
						 };

			var ordersList = await orders.ToListAsync();
			return View(ordersList);
		}


		// GET: GroupBuy/Details/5
		public async Task<IActionResult> Details(int id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var groupBuying = (from gb in _context.GroupBuyings
							   select new GroupBuyDTO
							   {
								   Id = id,
								   UserAccount = gb.AccountUserAccount,
								   ProductName = gb.ProductName,
								   ProductDescription = gb.ProductDescription,
								   TargetCount = gb.TargetCount,
								   Price = gb.Price,
								   Statement = gb.Statement,
								   ModifiedTime = gb.ModifiedTime,
								   ModifiedTimeView = gb.ModifiedTime.ToString("yyyy-MM-dd"),
								   Display = gb.Display,
                                   Photos = (from ph in _context.GroupBuyingPhotos
                                             where ph.IdGroupBuying == id
                                             select new GroupBuyPhotoDTO
                                             {
                                                 Id = ph.Id,
                                                 IdGroupBuying = ph.IdGroupBuying,
                                                 PhotoName = ph.PhotoName,
                                                 ModifiedTime = ph.ModifiedTime,

                                             }).ToList()
                               }).FirstOrDefault();
			if (groupBuying == null)
			{
				return NotFound();
			}

			return View(groupBuying);
		}

		// GET: GroupBuy/Create
		public IActionResult Create()
		{
			var group = (from gb in _context.GroupBuyings
						 select new GroupBuyDTO
						 {
							 ModifiedTime = DateTime.Now,
							 ModifiedTimeView = DateTime.Now.ToString("yyyy-MM-dd")
						 }).FirstOrDefault();
			return View(group);
		}

		// POST: GroupBuy/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([FromForm] GroupBuyDTO model)
		{

			if (model == null)
			{
				return NotFound();
			}

			var buy = new GroupBuying
			{
				Id = model.Id,
				AccountUserAccount = model.UserAccount,
				ProductName = model.ProductName,
				ProductDescription = model.ProductDescription,
				Price = model.Price,
				TargetCount = model.TargetCount,
				Statement = model.Statement,
				ModifiedTime = model.ModifiedTime,
				Display = model.Display,
			};

			_context.Add(buy);
			await _context.SaveChangesAsync();
			var newId = buy.Id;//加入相片用
			if (model.PhotoFiles != null && model.PhotoFiles.Count > 0)
			{
				//這裡處理檔案寫入資料庫的處理ˋ
				var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");// upload file path here
				if (!Directory.Exists(uploadPath))
				{
					Directory.CreateDirectory(uploadPath);// check folder exist
				}
				foreach (var file in model.PhotoFiles)
				{
					var filePath = Path.Combine(uploadPath, file.FileName);
					using (var fileStream = new FileStream(filePath, FileMode.Create))
					{
						await file.CopyToAsync(fileStream);// write file into fileStream
					}
					// please let GroupBuyPhotoDTO complete

					var groupBuyPhoto = new GroupBuyingPhoto
					{
						PhotoName = file.FileName,
						IdGroupBuying = newId,
						ModifiedTime = DateTime.Now,
					};

					_context.GroupBuyingPhotos.Add(groupBuyPhoto);
				}

				await _context.SaveChangesAsync();
			}
			return RedirectToAction(nameof(Index));


		}

		// GET: GroupBuy/Edit/5
		public async Task<IActionResult> Edit(int id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var groupBuying = (from gb in _context.GroupBuyings
							   where gb.Id == id
							   select new GroupBuyDTO
							   {
								   Id = gb.Id,
								   UserAccount = gb.AccountUserAccount,
								   ProductName = gb.ProductName,
								   ProductDescription = gb.ProductDescription,
								   TargetCount = gb.TargetCount,
								   Price = gb.Price,
								   Statement = gb.Statement,
								   ModifiedTime = gb.ModifiedTime,
								   ModifiedTimeView = gb.ModifiedTime.ToString("yyyy-MM-dd"),
								   Display = gb.Display,
								   Photos=(from ph in _context.GroupBuyingPhotos
										  where ph.IdGroupBuying==id
										  select new GroupBuyPhotoDTO
										  {
											  Id = ph.Id,
											  IdGroupBuying=ph.IdGroupBuying,
											  PhotoName=ph.PhotoName,
											  ModifiedTime=ph.ModifiedTime,

										  }).ToList()
							   }).FirstOrDefault();

			if (groupBuying == null)
			{
				return NotFound();
			}

			return View(groupBuying);
		}

		// POST: GroupBuy/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int Id, [FromForm] GroupBuyDTO model)
		{
			if (Id == null)
			{
				return NotFound();
			}

			var buy = new GroupBuying
			{
				AccountUserAccount = model.UserAccount,
				ProductName = model.ProductName,
				ProductDescription = model.ProductDescription,
				Price = model.Price,
				TargetCount = model.TargetCount,
				Statement = model.Statement,
				ModifiedTime = model.ModifiedTime,
				Display = model.Display,
			};

			_context.Update(buy);
			await _context.SaveChangesAsync();
			var newId = model.Id;//加入相片用
			if (model.PhotoFiles != null && model.PhotoFiles.Count > 0)
			{
				//這裡處理檔案寫入資料庫的處理ˋ
				var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");// upload file path here
				if (!Directory.Exists(uploadPath))
				{
					Directory.CreateDirectory(uploadPath);// check folder exist
				}
				foreach (var file in model.PhotoFiles)
				{
					var filePath = Path.Combine(uploadPath, file.FileName);
					using (var fileStream = new FileStream(filePath, FileMode.Create))
					{
						await file.CopyToAsync(fileStream);// write file into fileStream
					}
					// please let GroupBuyPhotoDTO complete

					var groupBuyPhoto = new GroupBuyingPhoto
					{
						
						PhotoName = file.FileName,
						IdGroupBuying = newId,
						ModifiedTime = DateTime.Now,
					};

					_context.GroupBuyingPhotos.Add(groupBuyPhoto);
				}

				await _context.SaveChangesAsync();
			}
			return RedirectToAction(nameof(Index));


		}

		[HttpPost]
		public async Task<IActionResult> DeletePhoto(int id)
		{
			var photo = await _context.GroupBuyingPhotos.FindAsync(id);
			if (photo == null)
			{
				return NotFound();
			}

			_context.GroupBuyingPhotos.Remove(photo);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Edit), new { id = photo.IdGroupBuying });
		}

		// GET: GroupBuy/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}
			var groupBuying = (from gb in _context.GroupBuyings
							   where gb.Id == id
							   select new GroupBuyDTO
							   {
								   Id = gb.Id,
								   ProductName = gb.ProductName,
								   ProductDescription = gb.ProductDescription,
								   TargetCount = gb.TargetCount,
								   Price = gb.Price,
								   Statement = gb.Statement,
								   ModifiedTime = gb.ModifiedTime,
								   Display = gb.Display,
								   ModifiedTimeView = gb.ModifiedTime.ToString("yyyy-MM-dd"),
							   }).FirstOrDefault();
			var hasPendingOrders = _context.GroupBuyingDetails.Any(o => o.GroupBuyingId == id);

			if (hasPendingOrders)
			{
				TempData["PendingOrdersMessage"] = "該商品尚有未出貨訂單，無法刪除。";
				return RedirectToAction(nameof(Index));
			}
			if (groupBuying == null)
			{
				return NotFound();
			}

			return View(groupBuying);
		}

		// POST: GroupBuy/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var groupBuying = await _context.GroupBuyings.FindAsync(id);
			var photos=await _context.GroupBuyingPhotos.Where(o=>o.IdGroupBuying==id).ToListAsync();
			if (groupBuying != null)
			{
				_context.GroupBuyingPhotos.RemoveRange(photos);
				_context.GroupBuyings.Remove(groupBuying);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool GroupBuyingExists(int id)
		{
			return _context.GroupBuyings.Any(e => e.Id == id);
		}

	}
}
