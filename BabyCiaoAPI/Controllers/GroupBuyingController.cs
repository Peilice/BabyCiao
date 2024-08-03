using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BabyCiaoAPI.Models;
using BabyCiaoAPI.DTO;
using Microsoft.AspNetCore.Cors;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using Microsoft.SqlServer.Server;

namespace BabyCiaoAPI.Controllers
{
    [EnableCors("andy")]
    [Route("api/[controller]")]
    [ApiController]
    public class GroupBuyingController : ControllerBase
    {
        private readonly BabyciaoContext _context;

        private readonly ILogger<GroupBuyingController> _logger;
        public GroupBuyingController(BabyciaoContext context,ILogger<GroupBuyingController> logger)
        {
            _context = context;
            _logger = logger;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GBDTO>>> GetGroupBuyings()
        {
            try
            {
                var groupBuys = await _context.GroupBuyings
                    .Where(gb => gb.Display)
                    .ToListAsync();

                var groupBuyIds = groupBuys.Select(gb => gb.Id).ToList();

                var groupBuyingDetails = await _context.GroupBuyingDetails
                    .Where(gbd => groupBuyIds.Contains(gbd.GroupBuyingId))
                    .ToListAsync();

                var groupBuyingDetailFormats = await _context.GroupBuyingDetailFormats
                    .Where(gbdf => groupBuyingDetails.Select(gbd => gbd.Id).Contains(gbdf.GroupBuyingDetailId))
                    .ToListAsync();

                var groupBuyingPhotos = await _context.GroupBuyingPhotos
                    .Where(gbp => groupBuyIds.Contains(gbp.IdGroupBuying))
                    .ToListAsync();

                var result = groupBuys.Select(gb => new GBDTO
                {
                    Id = gb.Id,
                    ProductName = gb.ProductName,
                    ProductDescription = gb.ProductDescription,
                    TargetCount = gb.TargetCount,
                    Price = gb.Price,
                    Statement = gb.Statement,
                    ModifiedTime = gb.ModifiedTime,
                    ModifiedTimeView = gb.ModifiedTime.ToString("yyyy-MM-dd"),
                    Display = gb.Display,
                    DisplayString = gb.Display ? "☑" : "",
                    ProductType = gb.ProductType,
                    JoinQuantity = groupBuyingDetails
                        .Where(gbd => gbd.GroupBuyingId == gb.Id)
                        .Select(gbd => groupBuyingDetailFormats
                            .Where(gbdf => gbdf.GroupBuyingDetailId == gbd.Id)
                            .Select(gbdf => gbdf.Quantity)
                            .FirstOrDefault())
                        .Sum(q => q),
                    progress = gb.TargetCount > 0
                        ? (decimal)(groupBuyingDetails
                            .Where(gbd => gbd.GroupBuyingId == gb.Id)
                            .Select(gbd => groupBuyingDetailFormats
                                .Where(gbdf => gbdf.GroupBuyingDetailId == gbd.Id)
                                .Select(gbdf => gbdf.Quantity)
                                .FirstOrDefault())
                            .Sum(q => q)) / gb.TargetCount * 100
                        : 0,
                    photoUrl = _context.GroupBuyingPhotos
                            .Where(p => p.IdGroupBuying == gb.Id)
                            .OrderBy(p => p.PhotoName)
                            .Select(p => p.PhotoName)
                            .FirstOrDefault(),
                    Photos = groupBuyingPhotos
                        .Where(gbp => gbp.IdGroupBuying == gb.Id)
                        .Select(gbp => new GroupBuyPhotoDTO
                        {
                            Id = gbp.Id,
                            IdGroupBuying = gbp.IdGroupBuying,
                            PhotoName = gbp.PhotoName,
                            ModifiedTime = gbp.ModifiedTime,
                           
                        })
                        .ToList()
                }).ToList();

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching group buyings");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        //篩選商品
        //POST:api/GroupBuying/Filter
        [HttpPost("Filter")]
        public async Task<ActionResult<IEnumerable<GBFilterDTO>>> FilterProducts([FromBody] GBFilterDTO model)
        {
            try
            {
                var query = _context.GroupBuyings
                    .Where(gb => gb.Display &&
                                 ((model.Id == 0 || gb.Id == model.Id) ||
                                  (string.IsNullOrEmpty(model.ProductName) || gb.ProductName.Contains(model.ProductName)) ||
                                  (string.IsNullOrEmpty(model.ProductDescription) || gb.ProductDescription.Contains(model.ProductDescription))) &&
                                 (string.IsNullOrEmpty(model.ProductType) || gb.ProductType == model.ProductType));

                var groupBuys = await query
                    .Select(gb => new GBFilterDTO
                    {
                        Id = gb.Id,
                        Price = gb.Price,
                        ProductType = gb.ProductType,
                        TargetCount = gb.TargetCount,
                        ProductName = gb.ProductName,
                        ProductDescription = gb.ProductDescription,
                        photoUrl = _context.GroupBuyingPhotos
                            .Where(p => p.IdGroupBuying == gb.Id)
                            .OrderBy(p => p.PhotoName)
                            .Select(p => p.PhotoName)
                            .FirstOrDefault()
                    })
                    .ToListAsync();

                foreach (var item in groupBuys)
                {
                    if (item.TargetCount > 0)
                    {
                        var details = await _context.GroupBuyingDetails
                            .Where(gbd => gbd.GroupBuyingId == item.Id)
                            .Select(gbd => new
                            {
                                gbd.Id,
                                FirstQuantity = _context.GroupBuyingDetailFormats
                                    .Where(gbdf => gbdf.GroupBuyingDetailId == gbd.Id)
                                    .Select(gbdf => gbdf.Quantity)
                                    .FirstOrDefault()
                            })
                            .ToListAsync();

                        var totalQuantity = details.Sum(d => d.FirstQuantity);
                        item.progress = (decimal)totalQuantity / item.TargetCount * 100;
                    }
                    else
                    {
                        item.progress = 0;
                    }
                }

                return Ok(groupBuys);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in FilterProducts");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        //商品詳情
        // GET: api/GroupBuying/5
        [HttpGet("Detail/{id}")]
        public async Task<ActionResult<GBDTO>> Detail(int id)
        {
            try
            {
                var groupBuying = await _context.GroupBuyings
                    .Where(gb => gb.Display && gb.Id == id)
                    .Select(gb => new GBDTO
                    {
                        Id = gb.Id,
                        ProductName = gb.ProductName,
                        Price = gb.Price,
                        ProductDescription = gb.ProductDescription,
                        TargetCount = gb.TargetCount,
                        Statement = gb.Statement,
                        ModifiedTime = gb.ModifiedTime,
                        ModifiedTimeView = gb.ModifiedTime.ToString("yyyy-MM-dd"),
                        Display = gb.Display,
                        DisplayString = gb.Display ? "☑" : "",
                        ProductType = gb.ProductType,
                        photoUrl = _context.GroupBuyingPhotos
                            .Where(p => p.IdGroupBuying == gb.Id)
                            .OrderBy(p => p.PhotoName)
                            .Select(p => p.PhotoName)
                            .FirstOrDefault(),
                        Photos = _context.GroupBuyingPhotos
                            .Where(ph => ph.IdGroupBuying == gb.Id)
                            .Select(ph => new GroupBuyPhotoDTO
                            {
                                Id = ph.Id,
                                IdGroupBuying = ph.IdGroupBuying,
                                PhotoName = ph.PhotoName,
                                ModifiedTime = ph.ModifiedTime,
                            })
                            .ToList(),
                        ProductFormats = _context.ProductFormats
                            .Where(of => of.IdGroupBuying == id)
                            .Select(of => new GroupBuyFormateDTO
                            {
                                Id = of.Id,
                                FormatName = of.FormatName,
                                FormatType = of.FormatType,
                            })
                            .ToList(),
                        DeadTime = gb.ModifiedTime.AddDays(30).ToString("yyyy-MM-dd")
                    })
                    .FirstOrDefaultAsync();

                if (groupBuying == null)
                {
                    return NotFound();
                }

                if (groupBuying.TargetCount > 0)
                {
                    var details = await _context.GroupBuyingDetails
                        .Where(gbd => gbd.GroupBuyingId == id)
                        .Select(gbd => new
                        {
                            gbd.Id,
                            FirstQuantity = _context.GroupBuyingDetailFormats
                                .Where(gbdf => gbdf.GroupBuyingDetailId == gbd.Id)
                                .Select(gbdf => gbdf.Quantity)
                                .FirstOrDefault()
                        })
                        .ToListAsync();

                    var totalQuantity = details.Sum(d => d.FirstQuantity);
                    groupBuying.progress = (decimal)totalQuantity / groupBuying.TargetCount * 100;
                    groupBuying.JoinQuantity = totalQuantity;
                }
                else
                {
                    groupBuying.progress = 0;
                    groupBuying.JoinQuantity = 0;
                }

                return Ok(groupBuying);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in Detail method for id: {Id}", id);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        //訂購頁面
        // GET: api/GroupBuying/5
        [HttpGet("Order/{id}")]
        public async Task<ActionResult<GBDTO>> Order(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order =await (from gb in _context.GroupBuyings
                              where gb.Id == id
                               select new GBDTO
                               {
                                   Id = id,
                                   UserAccount = gb.AccountUserAccount,
                                   ProductName = gb.ProductName,
                                   ProductDescription = gb.ProductDescription,
                                   TargetCount = gb.TargetCount,
                                   Price = gb.Price,
                                   Statement = gb.Statement,
                                   ProductType = gb.ProductType,
                                   ModifiedTime = DateTime.Now,
                                   ModifiedTimeView = DateTime.Now.ToString("yyyy-MM-dd"),
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
                               }).FirstOrDefaultAsync();
           
            if (order == null || !order.Display)
            {
				return NotFound(new { message = "該商品不存在" });
			}

            return Ok(order);
        }

		//送出訂單
		[HttpPost("SubmitOrder")]
		public async Task<ActionResult<IEnumerable<GBOrderDTO>>> SubmitOrder([FromBody] GBOrderDTO model)
        {
            if (model == null)
            {
                return NotFound();
            }
            var order = new GroupBuyingDetail
            {
                Id=model.Id,
                GroupBuyingId=model.GroupBuyingId,
                AccountUserAccount = model.UserAccount,
                Address=model.Address,
                Note=model.Note!=null? model.Note:"無",
                ModifiedTime = DateTime.Now,
                Statement = "已參加",
            };
            _context.GroupBuyingDetails.Add(order);
            await _context.SaveChangesAsync();
            var newId = order.Id;

            if (model.OrderFormats != null)
            {
                foreach (var f in model.OrderFormats)
                {
                    var format = new GroupBuyingDetailFormat
                    {
                        GroupBuyingDetailId = newId,
                        FormatId = f.FormatId,
                        Quantity = f.Quantity,
                    };
                    _context.GroupBuyingDetailFormats.Add(format);
                }
                await _context.SaveChangesAsync();
            }
            else
            {
                var singleFormat=new GroupBuyingDetailFormat
				{
					GroupBuyingDetailId = newId,
					FormatId = 0,
					Quantity = 0,
				};
				_context.GroupBuyingDetailFormats.Add(singleFormat);
			await _context.SaveChangesAsync();
			}
		
			return Ok(new { groupBuyingDetailId = newId });
			

        }


		//// PUT: api/GroupBuying/5
		//// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		//[HttpPut("{id}")]
		//public async Task<IActionResult> PutGroupBuying(int id, GroupBuying groupBuying)
		//{
		//    if (id != groupBuying.Id)
		//    {
		//        return BadRequest();
		//    }

		//    _context.Entry(groupBuying).State = EntityState.Modified;

		//    try
		//    {
		//        await _context.SaveChangesAsync();
		//    }
		//    catch (DbUpdateConcurrencyException)
		//    {
		//        if (!GroupBuyingExists(id))
		//        {
		//            return NotFound();
		//        }
		//        else
		//        {
		//            throw;
		//        }
		//    }

		//    return NoContent();
		//}

		//// POST: api/GroupBuying
		//// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		//[HttpPost]
		//public async Task<ActionResult<GroupBuying>> PostGroupBuying(GroupBuying groupBuying)
		//{
		//    _context.GroupBuyings.Add(groupBuying);
		//    await _context.SaveChangesAsync();

		//    return CreatedAtAction("GetGroupBuying", new { id = groupBuying.Id }, groupBuying);
		//}

		// DELETE: api/GroupBuying/5
		[HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGroupBuying(int id)
        {
            var groupBuying = await _context.GroupBuyings.FindAsync(id);
            if (groupBuying == null)
            {
                return NotFound();
            }

            _context.GroupBuyings.Remove(groupBuying);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GroupBuyingExists(int id)
        {
            return _context.GroupBuyings.Any(e => e.Id == id);
        }
    }
}
