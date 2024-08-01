using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BabyCiao.Models;
using BabyCiao.Models.DTO;
using System.Net;
using System.Text;
using Microsoft.SqlServer.Server;

namespace BabyCiao.Controllers
{
	public class GroupBuyController : Controller
	{
		private readonly BabyciaoContext _context;
		private IWebHostEnvironment _webHostEnvironment;

		public GroupBuyController(BabyciaoContext context, IWebHostEnvironment webHostEnvironment)
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
								ProductFormats=(from pf in _context.ProductFormats where pf.IdGroupBuying==gb.Id select new GroupBuyFormateDTO
								{
									Id=pf.Id,
									IdGroupBuying=pf.IdGroupBuying,
									FormatType=pf.FormatType,
									FormatName=pf.FormatName,
								}).ToList(),
								Id = gb.Id,
								UserAccount = gb.AccountUserAccount,//add
								ProductType=gb.ProductType,//add
								ProductName = gb.ProductName,
								ProductDescription = gb.ProductDescription,
								TargetCount = gb.TargetCount,
								Price = gb.Price,
								Statement = gb.Statement,
								ModifiedTime = gb.ModifiedTime,
								ModifiedTimeView = gb.ModifiedTime.ToString("yyyy-MM-dd"),
								DeadTime = gb.ModifiedTime.AddDays(30).ToString("yyyy-MM-dd"),
								Display = gb.Display,

								DisplayString = gb.Display ? "☑" : "",
								JoinQuantity = (_context.GroupBuyingDetails
                    .Where(gbd => gbd.GroupBuyingId == gb.Id)
                    .SelectMany(gbd => _context.GroupBuyingDetailFormats
                        .Where(gbdf => gbdf.GroupBuyingDetailId == gbd.Id)
                        .Select(gbdf => gbdf.Quantity))
                    .Sum()),
                                photoUrl = gbp.PhotoName != null ? $"<img src=\" /uploads/{gbp.PhotoName}\" width=\"100\" />" : "<img src=\" /img/noImage.jpg\" width=\"100\" />",
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
								ProductFormats = (from pf in _context.ProductFormats
												   where pf.IdGroupBuying == gb.Id
												   select new GroupBuyFormateDTO
												   {
													   Id = pf.Id,
													   IdGroupBuying = pf.IdGroupBuying,
													   FormatType = pf.FormatType,
													   FormatName = pf.FormatName,
												   }).ToList(),
								Id = gb.Id,
								UserAccount = gb.AccountUserAccount,//add
								ProductType = gb.ProductType,//add
								ProductName = gb.ProductName,
								ProductDescription = gb.ProductDescription,
								TargetCount = gb.TargetCount,
								Price = gb.Price,
								Statement = gb.Statement,
								ModifiedTime = gb.ModifiedTime,
								ModifiedTimeView = gb.ModifiedTime.ToString("yyyy-MM-dd"),
								DeadTime = gb.ModifiedTime.AddDays(30).ToString("yyyy-MM-dd"),
								Display = gb.Display,

								DisplayString = gb.Display ? "☑" : "",
								JoinQuantity = _context.GroupBuyingDetails
                    .Where(gbd => gbd.GroupBuyingId == gb.Id)
                    .SelectMany(gbd => _context.GroupBuyingDetailFormats
                        .Where(gbdf => gbdf.GroupBuyingDetailId == gbd.Id)
                        .Select(gbdf => gbdf.Quantity))
                    .Sum(),
                                photoUrl = gbp.PhotoName != null ? $"<img src=\" /uploads/{gbp.PhotoName}\" width=\"100\" />" : "<img src=\" /img/noImage.jpg\" width=\"100\" />",
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
                             OrderFormat= (from d in _context.GroupBuyingDetails
										   join dt in _context.GroupBuyingDetailFormats on d.Id equals dt.GroupBuyingDetailId
										   join ft in _context.ProductFormats on dt.FormatId equals ft.Id
                                           where d.GroupBuyingId == gb.Id
                                           select new GroupBuyOrderFormatDTO
                                           {
											   Id=dt.Id,
											   GroupBuyingDetailId=d.Id,
                                               FormatType=ft.FormatType,
                                               FormatName=ft.FormatName,
                                               Quantity=dt.Quantity,

                                           }).ToList(),

                             JoinId = gbd.Id,
                             ProductName= gb.ProductName,
                             JoinGroupId = gbd.GroupBuyingId,
							 JoinUserAccount = gbd.AccountUserAccount,
                             TotalQuantity = (_context.GroupBuyingDetailFormats
                    .Where(f => f.GroupBuyingDetailId == gbd.Id)
                        .Select(gbdf => gbdf.Quantity))
                    .Sum(),
                             OrderPrice = (_context.GroupBuyingDetailFormats
                    .Where(f => f.GroupBuyingDetailId == gbd.Id)
                        .Select(gbdf => gbdf.Quantity))
                    .Sum() * gb.Price,
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
                             OrderFormat = (from d in _context.GroupBuyingDetails
                                            join dt in _context.GroupBuyingDetailFormats on d.Id equals dt.GroupBuyingDetailId
                                            join ft in _context.ProductFormats on dt.FormatId equals ft.Id
                                            where d.GroupBuyingId == gb.Id
                                            select new GroupBuyOrderFormatDTO
                                            {
                                                Id = dt.Id,
                                                GroupBuyingDetailId = d.Id,
                                                FormatType = ft.FormatType,
                                                FormatName = ft.FormatName,
                                                Quantity = dt.Quantity,

                                            }).ToList(),

                             JoinId = gbd.Id,
                             ProductName = gb.ProductName,
                             JoinGroupId = gbd.GroupBuyingId,
                             JoinUserAccount = gbd.AccountUserAccount,
                             TotalQuantity = (_context.GroupBuyingDetailFormats
                    .Where(f => f.GroupBuyingDetailId == gbd.Id)
                        .Select(gbdf => gbdf.Quantity))
                    .Sum(),
                             OrderPrice = (_context.GroupBuyingDetailFormats
                    .Where(f => f.GroupBuyingDetailId == gbd.Id)
                        .Select(gbdf => gbdf.Quantity))
                    .Sum() * gb.Price,
                             JoinModifiedTime = gbd.ModifiedTime,
                             ViewJoinModifiedTime = gbd.ModifiedTime.ToString("yyyy-MM-dd"),
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
								   ProductType = gb.ProductType,
								   ModifiedTime = gb.ModifiedTime,
								   ModifiedTimeView = gb.ModifiedTime.ToString("yyyy-MM-dd"),
								   DeadTime = gb.ModifiedTime.AddDays(30).ToString("yyyy-MM-dd"),
								   Display = gb.Display,
								   Photos = (from ph in _context.GroupBuyingPhotos
											 where ph.IdGroupBuying == id
											 select new GroupBuyPhotoDTO
											 {
												 Id = ph.Id,
												 IdGroupBuying = ph.IdGroupBuying,
												 PhotoName = ph.PhotoName,
												 ModifiedTime = ph.ModifiedTime,

											 }).ToList(),
								   ProductFormats = (from of in _context.ProductFormats
													  where of.IdGroupBuying == id
													  select new GroupBuyFormateDTO
													  {
														  Id = of.Id,
														  FormatName = of.FormatName,
														  FormatType = of.FormatType,
													  }).ToList(),
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] GroupBuyDTO model)
        {
            if (model == null)
            {
                return NotFound();
            }

            try
            {
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
                _context.GroupBuyings.Add(buy);

                await _context.SaveChangesAsync();
                var newId = buy.Id;

                if (model.ProductFormats != null&& model.ProductFormats[0].FormatName!=null)
                {
                    foreach (var f in model.ProductFormats)
                    {
                        var format = new ProductFormat
                        {
                            IdGroupBuying = newId,
                            FormatType = f.FormatType,
                            FormatName = f.FormatName,
                        };
                        _context.ProductFormats.Add(format);
                    }
                    await _context.SaveChangesAsync();
                }

                if (model.PhotoFiles != null && model.PhotoFiles.Count > 0)
                {
                    var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                    if (!Directory.Exists(uploadPath))
                    {
                        Directory.CreateDirectory(uploadPath);
                    }

                    foreach (var file in model.PhotoFiles)
                    {
                        var filePath = Path.Combine(uploadPath, file.FileName);
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }

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
            catch (Exception ex)
            {
                // 這裡可以記錄日誌或是處理例外
                ModelState.AddModelError("", "發生錯誤: " + ex.Message);
                return View(model);
            }
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
								   ProductType= gb.ProductType,
								   Display = gb.Display,
								   ProductFormats = (from of in _context.ProductFormats
													  where of.IdGroupBuying == id
													  select new GroupBuyFormateDTO
													  {
														  Id = of.Id,
														  FormatName = of.FormatName,
														  FormatType = of.FormatType,
													  }).ToList() ?? new List<GroupBuyFormateDTO>(),
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
        [HttpGet]
        public async Task<IActionResult> CheckPendingOrders(int formatId)
        {

            var hasPendingOrders = await _context.GroupBuyingDetailFormats
                .Where(d => d.FormatId == formatId)
                .Join(
                    _context.GroupBuyingDetails,
                    format => format.GroupBuyingDetailId,
                    detail => detail.Id,
                    (format, detail) => new { format, detail }
                )
                .AnyAsync(joined => joined.detail.Statement == "進行中");

            return Json(new { hasPendingOrders });
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


			var buy = await _context.GroupBuyings.FindAsync(Id);
			if (buy == null)
			{
				return NotFound();
			}

			// 更新實例的屬性
			buy.AccountUserAccount = model.UserAccount;
			buy.ProductName = model.ProductName;
			buy.ProductDescription = model.ProductDescription;
			buy.Price = model.Price;
			buy.TargetCount = model.TargetCount;
			buy.Statement = model.Statement;
			buy.ModifiedTime = model.ModifiedTime;
			buy.Display = model.Display;
			buy.ProductType = model.ProductType;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!GroupBuyingExists(buy.Id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}
			var newId = model.Id;//加入相片用


			if (model.ProductFormats != null)
			{
				// 根據 IdGroupBuying 查找現有的規格
				var existingFormats = await _context.ProductFormats
					.Where(o => o.IdGroupBuying == Id)
					.ToListAsync();

				// 根據 model 中的規格 ID 列表
				var modelFormatIds = model.ProductFormats.Select(f => f.Id).ToList();

				// 確定需要刪除的規格
				var formatsToRemove = existingFormats
					.Where(e => !modelFormatIds.Contains(e.Id))
					.ToList();

				// 更新現有規格和新增新規格
				foreach (var f in model.ProductFormats)
				{
					var existingFormat = existingFormats.FirstOrDefault(e => e.Id == f.Id);
					if (existingFormat != null)
					{
						// 更新現有規格
						existingFormat.FormatType = f.FormatType;
						existingFormat.FormatName = f.FormatName;
						// 從刪除列表中移除，以免被刪除
						formatsToRemove.Remove(existingFormat);
					}
					else
					{
						// 新增不存在的規格
						var newFormat = new ProductFormat
						{
							IdGroupBuying = Id, // 使用傳入的 Id
							FormatType = f.FormatType,
							FormatName = f.FormatName,
						};
						_context.ProductFormats.Add(newFormat);
					}
				}

				// 刪除不再需要的規格
				if (formatsToRemove.Any())
				{
					_context.ProductFormats.RemoveRange(formatsToRemove);
				}

				await _context.SaveChangesAsync();
			}

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
			var photos = await _context.GroupBuyingPhotos.Where(o => o.IdGroupBuying == id).ToListAsync();
            var formats = await _context.ProductFormats.Where(o => o.IdGroupBuying == id).ToListAsync();
            if (groupBuying != null)
			{
				if(photos != null)
				{

				_context.GroupBuyingPhotos.RemoveRange(photos);
				}
                if (formats != null)
                {
                    _context.ProductFormats.RemoveRange(formats);
                }
                _context.GroupBuyings.Remove(groupBuying);
			}
           

            await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool GroupBuyingExists(int id)
		{
			return _context.GroupBuyings.Any(e => e.Id == id);
		}


		///////////////////////////////////////////////
		///訂單管理
		//////////////////////////////////////////////

		// GET: GroupBuy/Ship/5
		public async Task<IActionResult> Ship(int id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var order = (from gbd in _context.GroupBuyingDetails
						 join gb in _context.GroupBuyings on gbd.GroupBuyingId equals gb.Id
						 join uf in _context.UserInformations on gbd.AccountUserAccount equals uf.AccountUser
						 where gbd.Id == id
						 select new GroupBuyDTO
						 {
							 OrderFormat=(from of in _context.GroupBuyingDetailFormats join pf in _context.ProductFormats on of.FormatId equals pf.Id where of.GroupBuyingDetailId==id
										  select new GroupBuyOrderFormatDTO
										  {
											  Id=of.Id,
											  GroupBuyingDetailId=of.GroupBuyingDetailId,
											  FormatId=of.FormatId,
											  FormatType=pf.FormatType,
											  FormatName=pf.FormatName,
											  Quantity=of.Quantity,
										  }).ToList(),
							 JoinId = gbd.Id,
							 ProductName = gb.ProductName,
							 JoinUserAccount = gbd.AccountUserAccount,
                             TotalQuantity = (_context.GroupBuyingDetailFormats
.Where(f => f.GroupBuyingDetailId == gbd.Id)
    .Select(gbdf => gbdf.Quantity))
.Sum(),
                             OrderPrice = (_context.GroupBuyingDetailFormats
.Where(f => f.GroupBuyingDetailId == gbd.Id)
    .Select(gbdf => gbdf.Quantity))
.Sum() * gb.Price,
                             JoinModifiedTime = gbd.ModifiedTime,
							 ViewJoinModifiedTime = gbd.ModifiedTime.ToString("yyyy-MM-dd"),
							 Statement = gbd.Statement,
							 Price = gb.Price,
							 Address = uf.Address,
						 }).FirstOrDefault();
			if (order == null)
			{
				return NotFound();
			}

			return View(order);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Shipped(int JoinId)
		{//傳入訂單編號
			var shipped = await _context.GroupBuyingDetails.FindAsync(JoinId);
			if (shipped != null)
			{
				shipped.Statement = "已出貨";
			}
			var groupBuyDetail = await _context.GroupBuyingDetails.Where(o=>o.Id==JoinId).FirstAsync();//訂單明細
			var gb = await _context.GroupBuyings.Where(d => d.Id == groupBuyDetail.GroupBuyingId).FirstAsync();//商品
			var user=await _context.UserInformations.Where(u=>u.AccountUser==groupBuyDetail.AccountUserAccount&&groupBuyDetail.Id==JoinId).FirstAsync();//訂購人
			var sb = new StringBuilder();
			sb.AppendLine($"訂單編號: {groupBuyDetail.Id}");
			sb.AppendLine($"商品名稱: {gb.ProductName}");
			sb.AppendLine($"訂購人: {user.UserFirstName}{user.UserLastName}");
			//sb.AppendLine($"訂購數量: {groupBuyDetail.Quantity}");
			sb.AppendLine($"商品價格: {gb.Price}");
			//sb.AppendLine($"訂單價格: {gb.Price*groupBuyDetail.Quantity}");
			sb.AppendLine($"地址: {user.Address}");
			sb.AppendLine($"訂購日: {groupBuyDetail.ModifiedTime}");
			sb.AppendLine($"出貨日: {DateTime.Now.ToString("yyyy-MM-dd")}");

			var byteArray = Encoding.UTF8.GetBytes(sb.ToString());
			var stream = new MemoryStream(byteArray);

			await _context.SaveChangesAsync();
			return File(stream, "text/plain", $"{groupBuyDetail.Id}出貨單.txt");
			//return RedirectToAction(nameof(Orders));
		}
		public async Task<IActionResult> Cancel(int id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var order = (from gbd in _context.GroupBuyingDetails
						 join gb in _context.GroupBuyings on gbd.GroupBuyingId equals gb.Id
						 join uf in _context.UserInformations on gbd.AccountUserAccount equals uf.AccountUser
						 where gbd.Id == id
						 select new GroupBuyDTO
						 {
							 JoinId = gbd.Id,
							 ProductName = gb.ProductName,
							 JoinUserAccount = gbd.AccountUserAccount,
							// Quantity = gbd.Quantity,
							// OrderPrice = gbd.Quantity * gb.Price,
							 JoinModifiedTime = gbd.ModifiedTime,
							 ViewJoinModifiedTime = gbd.ModifiedTime.ToString("yyyy-MM-dd"),
							 Statement = gbd.Statement,
							 Price = gb.Price,
							 Address = uf.Address,
						 }).FirstOrDefault();
			if (order == null)
			{
				return NotFound();
			}

			return View(order);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Canceled(int JoinId)
		{
			var shipped = await _context.GroupBuyingDetails.FindAsync(JoinId);
			if (shipped != null)
			{
				shipped.Statement = "已取消";
			}
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Orders));
		}
	}
}
