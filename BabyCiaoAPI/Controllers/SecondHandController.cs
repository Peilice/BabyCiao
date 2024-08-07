using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BabyCiaoAPI.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http.HttpResults;
using BabyCiaoAPI.DTO;

namespace BabyCiaoAPI.Controllers
{
	[EnableCors("andy")]
	[Route("api/[controller]")]
    [ApiController]
    public class SecondHandController : ControllerBase
    {
		private IWebHostEnvironment _webHostEnvironment;
		private readonly BabyciaoContext _context;

		private readonly ILogger<GroupBuyingController> _logger;
		public SecondHandController(BabyciaoContext context, ILogger<GroupBuyingController> logger, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
			_logger = logger;
			_webHostEnvironment = webHostEnvironment;
		}

        // GET: api/SecondHand
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SecondHandSuppliesDTO>>> GetSecondHandSupplies()
        {
            var result=await (from s in _context.SecondHandSupplies
							  join p in _context.SuppliesPhotos on s.Id equals p.IdSecondHandSupplies into pp
							  from p in pp.OrderBy(p => p.PhotoName).Take(1).DefaultIfEmpty()
                              where s.Display==true
							  select new SecondHandSuppliesDTO
                             {
                                 Id=s.Id,
                                 AccountUserAccount=s.AccountUserAccount,
                                 SuppliesName=s.SuppliesName,
								  ModifiedTimeView= s.ModifiedTime.ToString("yyyy-MM-dd"),

								  SuppliesDescription = s.SuppliesDescription,
								 StockQuantity=s.StockQuantity,
                                 Type=s.Type,
                                 Photo=p.PhotoName!=null? p.PhotoName:null,
							 }).ToListAsync();
			return Ok(result); ;
        }

		// GET: api/SecondHand
		[HttpPost("Filter")]
		public async Task<ActionResult<IEnumerable<SecondHandFilterDTO>>> Filter([FromBody] SecondHandFilterDTO model)
		{
            try
            {
                var query = _context.SecondHandSupplies.Where(s => s.Display == true && ((model.Id == 0 || s.Id == model.Id) ||
                                  (string.IsNullOrEmpty(model.SuppliesName) || s.SuppliesName.Contains(model.SuppliesName)) || (string.IsNullOrEmpty(model.AccountUserAccount) || s.AccountUserAccount.Contains(model.AccountUserAccount)) ||
                                  (string.IsNullOrEmpty(model.SuppliesDescription) || s.SuppliesDescription.Contains(model.SuppliesDescription))) &&
                                 (string.IsNullOrEmpty(model.Type) || s.Type == model.Type));
                var result=await query.Select(s=>new SecondHandFilterDTO
                {
					Id = s.Id,
					AccountUserAccount = s.AccountUserAccount,
					SuppliesName = s.SuppliesName,
					

					SuppliesDescription = s.SuppliesDescription,
					
					Type = s.Type,
					Photo = _context.SuppliesPhotos
							.Where(p => p.IdSecondHandSupplies == s.Id)
							.OrderBy(p => p.PhotoName)
							.Select(p => p.PhotoName)
							.FirstOrDefault()


				}).ToListAsync();

				return Ok(result); ;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "An error occurred in FilterProducts");
				return StatusCode(500, "An error occurred while processing your request.");
			}
		}
        // GET: api/SecondHand
        [HttpGet("MyProducts")]
        public async Task<ActionResult<IEnumerable<SecondHandSuppliesDTO>>> MyProducts(string user)
        {
            var result = await (from s in _context.SecondHandSupplies
                                join p in _context.SuppliesPhotos on s.Id equals p.IdSecondHandSupplies into pp
                                from p in pp.OrderBy(p => p.PhotoName).Take(1).DefaultIfEmpty()
                                where s.Display == true &&s.AccountUserAccount==user
                                select new SecondHandSuppliesDTO
                                {
                                    Id = s.Id,
                                    AccountUserAccount = user,
                                    SuppliesName = s.SuppliesName,
                                    ModifiedTimeView = s.ModifiedTime.ToString("yyyy-MM-dd"),

                                    SuppliesDescription = s.SuppliesDescription,
                                    StockQuantity = s.StockQuantity,
                                    Type = s.Type,
                                    Photo = p.PhotoName != null ? p.PhotoName : null,
                                }).ToListAsync();
            return Ok(result); ;
        }

		//     [HttpPost("CreateProduct")]
		//     public async Task<ActionResult<IEnumerable<SecondHandCreate>>> CreateProduct([FromBody] SecondHandCreate model)
		//     {
		//         if (model == null)
		//         {
		//             return NotFound();
		//         }
		//         var product = new SecondHandSupply
		//         {
		//             AccountUserAccount=model.AccountUserAccount,
		//             SuppliesName=model.SuppliesName,
		//             SuppliesDescription=model.SuppliesDescription,
		//             StockQuantity = model.StockQuantity,
		//             ModifiedTime = DateTime.Now,
		//             Type = model.Type,
		//             Display = true,
		//         };
		//         _context.SecondHandSupplies.Add(product);
		//         await _context.SaveChangesAsync();
		//         var newId = product.Id;
		//         //儲存照片
		//if (model.PhotoFiles != null && model.PhotoFiles.Count > 0)
		//{
		//	var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
		//	if (!Directory.Exists(uploadPath))
		//	{
		//		Directory.CreateDirectory(uploadPath);
		//	}

		//	foreach (var file in model.PhotoFiles)
		//	{
		//		var filePath = Path.Combine(uploadPath, file.FileName);
		//		using (var fileStream = new FileStream(filePath, FileMode.Create))
		//		{
		//			await file.CopyToAsync(fileStream);
		//		}

		//		var groupBuyPhoto = new SuppliesPhoto
		//		{
		//			PhotoName = file.FileName,
		//			IdSecondHandSupplies = newId,
		//			ModifiedTime = DateTime.Now.ToString("G"),
		//		};

		//		_context.SuppliesPhotos.Add(groupBuyPhoto);
		//	}

		//	await _context.SaveChangesAsync();
		//}
		////if (model.Photos != null && model.Photos.Count> 0)
		////{
		////    foreach (var f in model.Photos)
		////    {
		////        var photo = new SuppliesPhoto
		////        {
		////            IdSecondHandSupplies= newId,
		////            PhotoName=f.PhotoName,
		////            ModifiedTime=DateTime.Now.ToString("yyyy-MM-dd"),
		////        };
		////        _context.SuppliesPhotos.Add(photo);
		////    }
		////    await _context.SaveChangesAsync();
		////}


		//return Ok(new { Id = newId });


		//     }




		[HttpPost("CreateProduct")]
		public async Task<ActionResult> CreateProduct(
	[FromForm] string accountUserAccount,
	[FromForm] string suppliesName,
	[FromForm] string suppliesDescription,
	[FromForm] int stockQuantity,
	[FromForm] string type,
	[FromForm] bool display,
	
	[FromForm] List<IFormFile> photoFiles)
		{
			var product = new SecondHandSupply
			{
				AccountUserAccount = accountUserAccount,
				SuppliesName = suppliesName,
				SuppliesDescription = suppliesDescription,
				StockQuantity = stockQuantity,
				ModifiedTime = DateTime.Now,
				Type = type,
				Display = display,
			};

			_context.SecondHandSupplies.Add(product);
			await _context.SaveChangesAsync();
			var newId = product.Id;

			try
			{
				var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
				if (!Directory.Exists(uploadPath))
				{
					Directory.CreateDirectory(uploadPath);
				}
			}
			catch (Exception ex)
			{
				// 記錄錯誤
				Console.WriteLine($"Error creating directory: {ex.Message}");
				return StatusCode(500, new { message = "Error creating directory", details = ex.Message });
			}

			// 儲存照片
			if (photoFiles != null && photoFiles.Count > 0)
			{//錯誤尚未解決
				var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
				if (!Directory.Exists(uploadPath))
				{
					Directory.CreateDirectory(uploadPath);
				}

				foreach (var file in photoFiles)
				{
					var filePath = Path.Combine(uploadPath, file.FileName);
					using (var fileStream = new FileStream(filePath, FileMode.Create))
					{
						await file.CopyToAsync(fileStream);
					}

					var photo = new SuppliesPhoto
					{
						PhotoName = file.FileName,
						IdSecondHandSupplies = newId,
						ModifiedTime = DateTime.Now.ToString("G"),
					};

					_context.SuppliesPhotos.Add(photo);
				}

				await _context.SaveChangesAsync();
			}

			return Ok(new { Id = newId });
		}
		//// GET: api/SecondHand/5
		//[HttpGet("{id}")]
		//      public async Task<ActionResult<SecondHandSupply>> GetSecondHandSupply(int id)
		//      {
		//          var secondHandSupply = await _context.SecondHandSupplies.FindAsync(id);

		//          if (secondHandSupply == null)
		//          {
		//              return NotFound();
		//          }

		//          return secondHandSupply;
		//      }

		//      // PUT: api/SecondHand/5
		//      // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		//      [HttpPut("{id}")]
		//      public async Task<IActionResult> PutSecondHandSupply(int id, SecondHandSupply secondHandSupply)
		//      {
		//          if (id != secondHandSupply.Id)
		//          {
		//              return BadRequest();
		//          }

		//          _context.Entry(secondHandSupply).State = EntityState.Modified;

		//          try
		//          {
		//              await _context.SaveChangesAsync();
		//          }
		//          catch (DbUpdateConcurrencyException)
		//          {
		//              if (!SecondHandSupplyExists(id))
		//              {
		//                  return NotFound();
		//              }
		//              else
		//              {
		//                  throw;
		//              }
		//          }

		//          return NoContent();
		//      }

		//      // POST: api/SecondHand
		//      // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		//      [HttpPost]
		//      public async Task<ActionResult<SecondHandSupply>> PostSecondHandSupply(SecondHandSupply secondHandSupply)
		//      {
		//          _context.SecondHandSupplies.Add(secondHandSupply);
		//          await _context.SaveChangesAsync();

		//          return CreatedAtAction("GetSecondHandSupply", new { id = secondHandSupply.Id }, secondHandSupply);
		//      }

		//      // DELETE: api/SecondHand/5
		//      [HttpDelete("{id}")]
		//      public async Task<IActionResult> DeleteSecondHandSupply(int id)
		//      {
		//          var secondHandSupply = await _context.SecondHandSupplies.FindAsync(id);
		//          if (secondHandSupply == null)
		//          {
		//              return NotFound();
		//          }

		//          _context.SecondHandSupplies.Remove(secondHandSupply);
		//          await _context.SaveChangesAsync();

		//          return NoContent();
		//      }

		//      private bool SecondHandSupplyExists(int id)
		//      {
		//          return _context.SecondHandSupplies.Any(e => e.Id == id);
		//      }
	}
}
