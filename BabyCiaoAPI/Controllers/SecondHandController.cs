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
        public async Task<ActionResult<IEnumerable<SecondHandSuppliesDTO>>> GetSecondHandSupplies(string? userAccount)
        {
            List<int> userFavorites = new List<int>();

            if (userAccount != null)
            {
                if (!string.IsNullOrEmpty(userAccount))
                {
                    // 獲取用戶的最愛商品
                    userFavorites = await _context.SecondHandFavorites
                        .Where(fav => fav.AccountUserAccount == userAccount)
                        .Select(fav => fav.IdSecondHandSupplies)
                        .ToListAsync();
                }

            }
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
                                    IsFavorite = userFavorites.Contains(s.Id)
                                }).ToListAsync();
            return Ok(result);
        }

        // GET: api/SecondHand/Filter
        [HttpPost("Filter")]
        public async Task<ActionResult<IEnumerable<SecondHandFilterDTO>>> Filter([FromBody] SecondHandFilterDTO model, string? userAccount)
        {
            try
            {
                List<int> userFavorites = new List<int>();

                if (userAccount != null)
                {
                    if (!string.IsNullOrEmpty(userAccount))
                    {
                        // 獲取用戶的最愛商品
                        userFavorites = await _context.SecondHandFavorites
                            .Where(fav => fav.AccountUserAccount == userAccount)
                            .Select(fav => fav.IdSecondHandSupplies)
                            .ToListAsync();
                    }

                }
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
                            .FirstOrDefault(),
                    IsFavorite = userFavorites.Contains(s.Id)


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
        public async Task<ActionResult<IEnumerable<SecondHandExchangeDTO>>> Exchange(int id)
        {/////傳入想換商品的ID
            var result = await (from s in _context.SecondHandSupplies
                                join p in _context.SuppliesPhotos on s.Id equals p.IdSecondHandSupplies into pp
                                from p in pp.OrderBy(p => p.PhotoName).Take(1).DefaultIfEmpty()
                                where s.Display == true&&s.Id==id
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
                                }).FirstOrDefaultAsync();
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
                    BuyerId = model.BuyerId,
                    SellerId = model.SellerId,
                    WantGetId = model.WantGetId,
                    GetQuantity = model.GetQuantity,
                    WantGiveId = model.WantGiveId,
                    GiveQuantity = model.GiveQuantity,
                    ModifiedTime = DateTime.Now,
                    Statement = "申請中",
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
        //申請中
        [HttpGet("MyOrders")]
        public async Task<ActionResult<IEnumerable<GetSecondHandExchangeDTO>>> MyOrders(string user)
        {
            var result = await (from ex in _context.SecondHandExchangeOrders
                                join p in _context.SecondHandSupplies on ex.WantGetId equals p.Id
								join p2 in _context.SecondHandSupplies on ex.WantGiveId equals p2.Id
								where ex.BuyerId == user
                                select new GetSecondHandExchangeDTO
                                {
									Id=ex.Id,
                                    BuyerId=user,
                                    SellerId=ex.SellerId,
                                    WantGetId=ex.WantGetId,
                                    WantName=p.SuppliesName,
                                    GetQuantity=ex.GetQuantity,
                                    WantGiveId=ex.WantGiveId,
                                    GiveName=p2.SuppliesName,
                                    GiveQuantity=ex.GetQuantity,
                                    ModifiedTime=ex.ModifiedTime,
                                    View=ex.ModifiedTime.ToString("yyyy-MM-dd"),
                                    Statement=ex.Statement,


								}).ToListAsync();
            return Ok(result); ;
        }



        // GET: api/SecondHand
        //未確認
        [HttpGet("UncheckedOrders")]
        public async Task<ActionResult<IEnumerable<GetSecondHandExchangeDTO>>> UncheckedOrders(string user)
        {
            var result = await (from ex in _context.SecondHandExchangeOrders
                                join p in _context.SecondHandSupplies on ex.WantGetId equals p.Id
                                join p2 in _context.SecondHandSupplies on ex.WantGiveId equals p2.Id
                                where ex.SellerId == user
                                select new GetSecondHandExchangeDTO
                                {
                                    Id = ex.Id,
                                    BuyerId = ex.BuyerId,
                                    SellerId = user,
                                    WantGetId = ex.WantGetId,
                                    WantName = p.SuppliesName,
                                    GetQuantity = ex.GetQuantity,
                                    WantGiveId = ex.WantGiveId,
                                    GiveName = p2.SuppliesName,
                                    GiveQuantity = ex.GetQuantity,
                                    ModifiedTime = ex.ModifiedTime,
                                    View = ex.ModifiedTime.ToString("yyyy-MM-dd"),
                                    Statement = ex.Statement,


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

        ////加到收藏/最愛
        [HttpPost("AddFav")]
        public async Task<ActionResult<IEnumerable<SecondGetFavDTO>>> AddFav(int id, string user)
        {
            if (id == 0 || string.IsNullOrEmpty(user))
            {
                return BadRequest(new { message = "Invalid ID or User" });
            }

            try
            {
                var fav = new SecondHandFavorite
                {
                    Id = 0,
                    IdSecondHandSupplies = id,
                    AccountUserAccount = user
                };
                _context.SecondHandFavorites.Add(fav);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding favorite.");
                return BadRequest(new { message = "An error occurred while processing your request.", error = ex.Message });
            }

            return NoContent();
        }
        ////刪除收藏/最愛
        [HttpDelete("DeleteFav")]
        public async Task<ActionResult<IEnumerable<SecondGetFavDTO>>> DeleteFav(int id, string user)
        {

            var fav = await _context.SecondHandFavorites.Where(f => f.IdSecondHandSupplies == id && f.AccountUserAccount == user).FirstOrDefaultAsync();
            if (fav == null)
            {
                return NotFound();
            }

            _context.SecondHandFavorites.Remove(fav);
            await _context.SaveChangesAsync();

            return NoContent();

        }
        //我的最愛
        [HttpGet("MyFavorite/{user}")]
        public async Task<ActionResult<SecondGetFavDTO>> MyFavorite(string user)
        {
            if (user == null)
            {
                return NotFound();
            }

            var myfavs = await (from myf in _context.SecondHandFavorites
                                join gb in _context.SecondHandSupplies on myf.IdSecondHandSupplies equals gb.Id
                                where myf.AccountUserAccount == user
                                select new SecondGetFavDTO
                                {
                                    Id = myf.Id,
                                    IdSecondHandSupplies = myf.IdSecondHandSupplies,
                                    UserName = user,
                                    ProductName = gb.SuppliesName,


                                }).ToListAsync();

            if (myfavs == null)
            {
                return NotFound(new { message = "尚無已加入之最愛商品" });
            }

            return Ok(myfavs);
        }

        //確認訂單
        [HttpPut("ConfirmOrder")]
        public async Task<ActionResult<SecondHandExchangeDTO>> ConfirmOrder(int id)
        {
            var order = await
                 _context.SecondHandExchangeOrders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            order.Statement = "確認成交";
            await _context.SaveChangesAsync();

            return Ok(order);
        }
        //取消訂單
        [HttpPut("CancleOrder")]
        public async Task<ActionResult<SecondHandExchangeDTO>> CancleOrder(int id)
        {
            var order = await
                 _context.SecondHandExchangeOrders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            order.Statement = "取消申請";
            await _context.SaveChangesAsync();

            return Ok(order);
        }
        //拒絕交換
        [HttpPut("RejectOrder")]
        public async Task<ActionResult<SecondHandExchangeDTO>> RejectOrder(int id)
        {
            var order = await
                 _context.SecondHandExchangeOrders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            order.Statement = "不成立";
            await _context.SaveChangesAsync();

            return Ok(order);
        }

    }
}
