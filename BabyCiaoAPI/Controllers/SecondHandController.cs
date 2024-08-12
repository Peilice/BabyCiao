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
using Microsoft.SqlServer.Server;

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
            var result = await (from s in _context.SecondHandSupplies
                                join p in _context.SuppliesPhotos on s.Id equals p.IdSecondHandSupplies into pp
                                from p in pp.OrderBy(p => p.PhotoName).Take(1).DefaultIfEmpty()
                                where s.Display == true
                                select new SecondHandSuppliesDTO
                                {
                                    Id = s.Id,
                                    AccountUserAccount = s.AccountUserAccount,
                                    SuppliesName = s.SuppliesName,
                                    ModifiedTimeView = s.ModifiedTime.ToString("yyyy-MM-dd"),

                                    SuppliesDescription = s.SuppliesDescription,
                                    StockQuantity = s.StockQuantity,
                                    Type = s.Type,
                                    Photo = p.PhotoName != null ? p.PhotoName : null,
                                }).ToListAsync();
            return Ok(result); 
        }

        // GET: api/SecondHand/Filter
        [HttpPost("Filter")]
        public async Task<ActionResult<IEnumerable<SecondHandFilterDTO>>> Filter([FromBody] SecondHandFilterDTO model)
        {
            try
            {
                var query = _context.SecondHandSupplies.Where(s => s.Display == true && ((model.Id == 0 || s.Id == model.Id) ||
                                  (string.IsNullOrEmpty(model.SuppliesName) || s.SuppliesName.Contains(model.SuppliesName)) || (string.IsNullOrEmpty(model.AccountUserAccount) || s.AccountUserAccount.Contains(model.AccountUserAccount)) ||
                                  (string.IsNullOrEmpty(model.SuppliesDescription) || s.SuppliesDescription.Contains(model.SuppliesDescription))) &&
                                 (string.IsNullOrEmpty(model.Type) || s.Type == model.Type));
                var result = await query.Select(s => new SecondHandFilterDTO
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

                return Ok(result); 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in FilterProducts");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
       
        
        // GET: api/SecondHand/Exchange
        [HttpGet("Exchange")]
        public async Task<ActionResult<IEnumerable<SecondHandExchangeDTO>>> Exchange(int id,string user)
        {/////未完成
            var result = await (from s in _context.SecondHandSupplies
                                join p in _context.SuppliesPhotos on s.Id equals p.IdSecondHandSupplies into pp
                                from p in pp.OrderBy(p => p.PhotoName).Take(1).DefaultIfEmpty()
                                where s.Display == true
                                select new SecondHandSuppliesDTO
                                {
                                    Id = s.Id,
                                    AccountUserAccount = s.AccountUserAccount,
                                    SuppliesName = s.SuppliesName,
                                    ModifiedTimeView = s.ModifiedTime.ToString("yyyy-MM-dd"),

                                    SuppliesDescription = s.SuppliesDescription,
                                    StockQuantity = s.StockQuantity,
                                    Type = s.Type,
                                    Photo = p.PhotoName != null ? p.PhotoName : null,
                                }).ToListAsync();
            return Ok(result);
        }
        // Post: api/SecondHand/Exchange
        [HttpPost("Exchange")]
        public async Task<ActionResult<IEnumerable<SecondHandExchangeDTO>>> Exchange([FromBody] SecondHandExchangeDTO model)
        {
            try
            {
                var exchange = new SecondHandExchangeOrder
                {
                    BuyerId=model.BuyerId,
                    SellerId=model.SellerId,
                    WantGetId=model.WantGetId,
                    GetQuantity = model.GetQuantity,
                    WantGiveId = model.WantGiveId,
                    GiveQuantity = model.GiveQuantity,
                    ModifiedTime=DateTime.Now,
                    Statement="申請中",
                };
                _context.SecondHandExchangeOrders.Add(exchange);
                await _context.SaveChangesAsync();
                //++++++++++++++
                return Ok(exchange); ;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in FilterProducts");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        // GET: api/SecondHand
        [HttpGet("MyOrders")]
        public async Task<ActionResult<IEnumerable<SecondHandSuppliesDTO>>> MyOrders()
        {//////未完成!! 未加入USER篩選
            var result = await (from s in _context.SecondHandSupplies
                                join p in _context.SuppliesPhotos on s.Id equals p.IdSecondHandSupplies into pp
                                from p in pp.OrderBy(p => p.PhotoName).Take(1).DefaultIfEmpty()
                                where s.Display == true
                                select new SecondHandSuppliesDTO
                                {
                                    Id = s.Id,
                                    AccountUserAccount = s.AccountUserAccount,
                                    SuppliesName = s.SuppliesName,
                                    ModifiedTimeView = s.ModifiedTime.ToString("yyyy-MM-dd"),

                                    SuppliesDescription = s.SuppliesDescription,
                                    StockQuantity = s.StockQuantity,
                                    Type = s.Type,
                                    Photo = p.PhotoName != null ? p.PhotoName : null,
                                }).ToListAsync();
            return Ok(result); ;
        }
        // GET: api/SecondHand
        [HttpGet("MyProducts")]
        public async Task<ActionResult<IEnumerable<SecondHandSuppliesDTO>>> MyProducts(string user)
        {
            var result = await (from s in _context.SecondHandSupplies
                                join p in _context.SuppliesPhotos on s.Id equals p.IdSecondHandSupplies into pp
                                from p in pp.OrderBy(p => p.PhotoName).Take(1).DefaultIfEmpty()
                                where s.Display == true && s.AccountUserAccount == user
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


        // GET: api/SecondHand/Detail
        [HttpGet("Detail")]
        public async Task<ActionResult<IEnumerable<SecondHandDetailDTO>>> GetDetail(int id)
        {
            var result = await (from s in _context.SecondHandSupplies
                                    //join p in _context.SuppliesPhotos on s.Id equals p.IdSecondHandSupplies

                                where s.Display == true && s.Id == id
                                select new SecondHandDetailDTO
                                {
                                    Id = s.Id,
                                    AccountUserAccount = s.AccountUserAccount,
                                    SuppliesName = s.SuppliesName,
                                    ModifiedTimeView = s.ModifiedTime.ToString("yyyy-MM-dd"),

                                    SuppliesDescription = s.SuppliesDescription,
                                    StockQuantity = s.StockQuantity,
                                    Type = s.Type,
                                    Photos = _context.SuppliesPhotos.Where(p => p.IdSecondHandSupplies == id).Select(ph => new SuppliesPhotoDTO
                                    {
                                        Id = ph.Id,
                                        IdSecondHandSupplies = ph.IdSecondHandSupplies,
                                        PhotoName = ph.PhotoName,
                                        ModifiedTime = ph.ModifiedTime,
                                    })
                            .ToList(),
                                }).FirstOrDefaultAsync();
            return Ok(result); ;
        }


        // GET: api/SecondHand/CreateProduct
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
            {
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
    

        //      // PUT: api/SecondHand/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSecondHandSupply(int id,
    [FromForm] string suppliesName,
    [FromForm] string suppliesDescription,
    [FromForm] int stockQuantity,
    [FromForm] string type,
    [FromForm] List<IFormFile> photoFiles)
        {///未完成!!!
            var product = await _context.SecondHandSupplies.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            // 更新產品資訊
            product.SuppliesName = suppliesName;
            product.SuppliesDescription = suppliesDescription;
            product.StockQuantity = stockQuantity;
            product.Type = type;
            product.ModifiedTime = DateTime.Now;

            _context.SecondHandSupplies.Update(product);
            await _context.SaveChangesAsync();

            // 照片處理
            try
            {

                var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                // 刪除舊的照片（可選）
                var existingPhotos = _context.SuppliesPhotos.Where(p => p.IdSecondHandSupplies == id).ToList();
                foreach (var existingPhoto in existingPhotos)
                {
                    var existingPhotoPath = Path.Combine(uploadPath, existingPhoto.PhotoName);
                    if (System.IO.File.Exists(existingPhotoPath))
                    {
                        System.IO.File.Delete(existingPhotoPath);
                    }
                    _context.SuppliesPhotos.Remove(existingPhoto);
                }

                // 儲存新的照片
                if (photoFiles != null && photoFiles.Count > 0)
                {
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
                            IdSecondHandSupplies = id,
                            ModifiedTime = DateTime.Now.ToString("G"),
                        };

                        _context.SuppliesPhotos.Add(photo);
                    }

                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                // 記錄錯誤
                Console.WriteLine($"Error processing files: {ex.Message}");
                return StatusCode(500, new { message = "Error processing files", details = ex.Message });
            }

            return Ok();
        }


        // DELETE: api/SecondHand/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSecondHandSupply(int id)
        {
            var secondHandSupply = await _context.SecondHandSupplies.FindAsync(id);
            var photos = _context.SuppliesPhotos.Where(p => p.IdSecondHandSupplies == id).ToList();
            var isExchange = _context.SecondHandExchangeOrders
                  .Any(e => (e.WantGetId == id || e.WantGiveId == id) && e.Statement == "申請中");

            if (isExchange)
            {
                // 如果有未處理的申請中訂單，返回一個狀態碼或消息
                return Ok(new { requiresAttention = true });
            }


            if (secondHandSupply == null)
            {
                return NotFound();
            }
            else
            {
                if (photos != null)
                {
                    _context.SuppliesPhotos.RemoveRange(photos);
                }
                _context.SecondHandSupplies.Remove(secondHandSupply);
            }
                    await _context.SaveChangesAsync();



            return NoContent();
        }

       
    }
}
