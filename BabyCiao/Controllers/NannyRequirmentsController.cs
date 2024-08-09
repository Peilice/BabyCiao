using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BabyCiao.Data;
using BabyCiao.Models;
using System.Linq;
using System.Threading.Tasks;
using BabyCiao.Models.DTO;

public class NannyRequirmentsController : Controller
{
    private readonly BabyciaoContext _context;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public NannyRequirmentsController(BabyciaoContext context, IWebHostEnvironment webHostEnvironment)
    {
        _context = context;
        _webHostEnvironment = webHostEnvironment;
    }



    // GET: NannyRequirments
    [HttpGet]

    //public async Task<IActionResult> Index()
    //{
    //    var babyCiaoContext = await _context.NannyRequirments
    //        .Include(n => n.NannyAccountUserAccountNavigation)
    //        .ToListAsync();

    //    return View(babyCiaoContext);
    //}
    public async Task<IActionResult> Index()
    {
        var babyCiaoContext = await _context.NannyRequirments
         .Include(n => n.NannyAccountUserAccountNavigation)
         .Select(n => new NannyRequirementDTO
         {
             Id= n.Id,
             //RequirementDate = n.RequirementDate,
             NannyAccountUserAccount = n.NannyAccountUserAccount,
             PoliceCriminalRecordCertificate = n.PoliceCriminalRecordCertificate,
             ChildCareCertificate = n.ChildCareCertificate,
             NationalIdentificationCard = n.NationalIdentificationCard,
             AddressesOfAgencies = n.AddressesOfAgencies,
             ValidPeriodsOfCertificates = n.ValidPeriodsOfCertificates,
             Statement = n.Statement,
             photoA =n.PoliceCriminalRecordCertificate,
             photoB=n.ChildCareCertificate,
             photoC=n.NationalIdentificationCard,

         }).ToListAsync();

        return View(babyCiaoContext);
        //return View(await babyCiaoContext.ToListAsync());
    }


    private string GetImageHtml(string fileName, string folder)
    {
        return !string.IsNullOrEmpty(fileName)
            ? $"<img src=\"/{folder}/{fileName}\" width=\"100\" />"
            : "<img src=\"/img/noImage.jpg\" width=\"100\" />";
    }

    //};
    ////        ? $"<img src=\"/保母證照/{nannyRequirments.PoliceCriminalRecordCertificate}\" width=\"100\" />"
    ////        : "<img src=\"/img/noImage.jpg\" width=\"100\" />";

    ////    requirement.ChildCareCertificate = requirement.ChildCareCertificate != null
    ////        ? $"<img src=\"/保母證照/{requirnannyRequirmentsement.ChildCareCertificate}\" width=\"100\" />"
    ////        : "<img src=\"/img/noImage.jpg\" width=\"100\" />";

    ////    rerequirement.NationalIdentificationCard != null
    //        ? $"<img src=\"/保母證照/{nannyRequirments.NationalIdentificationCard}\" width=\"100\" />"
    //        : "<img src=\"/img/noImage.jpg\" width=\"100\" />";
    //}


    // GET: NannyRequirments/Create
    public IActionResult Create()
    {
        ViewData["NannyAccountUserAccount"] = new SelectList(_context.UserAccounts, "Account", "Account");
        return View();
    }

    // POST: NannyRequirments/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([FromForm] NannyRequirementDTO model)
    {
        if (model == null)
        {
            return NotFound();
        }


        var Requirment = new NannyRequirment
        {
            Id = model.Id,
            //RequirementDate = model.RequirementDate,
            NannyAccountUserAccount = model.NannyAccountUserAccount,
            PoliceCriminalRecordCertificate = model.PoliceCriminalRecordCertificate,
            ChildCareCertificate = model.ChildCareCertificate,
            NationalIdentificationCard = model.NationalIdentificationCard,
            AddressesOfAgencies = model.AddressesOfAgencies,
            ValidPeriodsOfCertificates = model.ValidPeriodsOfCertificates,
            Statement = model.Statement,
        };

        if (model.NannyAccountUserAccount != null && model.ChildCareCertificate != null && model.NationalIdentificationCard != null)
        {
            //這裡處理檔案寫入資料庫的處理
            var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "/Nannnyandperant/良民證/");// upload file path here
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);// check folder exist
            }

            var filePath = Path.Combine(uploadPath, model.photo1.FileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await model.photo1.CopyToAsync(fileStream);// write file into fileStream
            }
            var uploadPath2 = Path.Combine(_webHostEnvironment.WebRootPath, "/Nannnyandperant/保母證/");// upload file path here
            if (!Directory.Exists(uploadPath2))
            {
                Directory.CreateDirectory(uploadPath2);// check folder exist
            }
            var filePath2 = Path.Combine(uploadPath2, model.photo2.FileName);
            using (var fileStream = new FileStream(filePath2, FileMode.Create))
            {
                await model.photo2.CopyToAsync(fileStream);// write file into fileStream
            }
            var uploadPath3 = Path.Combine(_webHostEnvironment.WebRootPath, "/Nannnyandperant/身分證/");// upload file path here
            if (!Directory.Exists(uploadPath3))
            {
                Directory.CreateDirectory(uploadPath3);// check folder exist
            }
            var filePath3 = Path.Combine(uploadPath3, model.photo3.FileName);
            using (var fileStream = new FileStream(filePath3, FileMode.Create))
            {
                await model.photo3.CopyToAsync(fileStream);// write file into fileStream
            }
            // please let GroupBuyPhotoDTO complete
            Requirment.PoliceCriminalRecordCertificate = model.photo1.FileName;
            Requirment.ChildCareCertificate = model.photo2.FileName;
            Requirment.NationalIdentificationCard = model.photo3.FileName;
            _context.Add(Requirment);

            Requirment.PoliceCriminalRecordCertificate = model.photoA;
            Requirment.ChildCareCertificate = model.photoB;
            Requirment.NationalIdentificationCard = model.photoC;

            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }


    // GET: NannyRequirments/Edit/5
    public async Task<IActionResult> Edit(int Id)
    {
        if (Id == null)
        {
            return NotFound();
        }
        var Requirment = (from nn in _context.NannyRequirments
                          where nn.Id == Id
                          select new NannyRequirementDTO
                          {
                              Id = nn.Id,
                              //RequirementDate = nn.RequirementDate,
                              NannyAccountUserAccount = nn.NannyAccountUserAccount,
                              PoliceCriminalRecordCertificate = nn.PoliceCriminalRecordCertificate,
                              ChildCareCertificate = nn.ChildCareCertificate,
                              NationalIdentificationCard = nn.NationalIdentificationCard,
                              AddressesOfAgencies = nn.AddressesOfAgencies,
                              ValidPeriodsOfCertificates = nn.ValidPeriodsOfCertificates,
                              Statement = nn.Statement,
                              photoA = nn.PoliceCriminalRecordCertificate,
                              photoB = nn.ChildCareCertificate,
                              photoC = nn.NationalIdentificationCard,
                          }).FirstOrDefault();


        if (Requirment == null)
        {
            return NotFound();
        }

        return View(Requirment);
    }

    // POST: NannyRequirments/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int Id, [FromForm] NannyRequirementDTO model)
    {
        if (Id == null)
        {
            return NotFound();
        }
        var Requirment = new NannyRequirment
        {
            Id = model.Id,
            //RequirementDate = model.RequirementDate,
            NannyAccountUserAccount = model.NannyAccountUserAccount,
            PoliceCriminalRecordCertificate = model.PoliceCriminalRecordCertificate,
            ChildCareCertificate = model.ChildCareCertificate,
            NationalIdentificationCard = model.NationalIdentificationCard,
            AddressesOfAgencies = model.AddressesOfAgencies,
            ValidPeriodsOfCertificates = model.ValidPeriodsOfCertificates,
            Statement = model.Statement,

        };
        _context.Update(Requirment);
        if (model.photoA != null && model.photoB != null && model.photoC != null)
        {
            //這裡處理檔案寫入資料庫的處理ˋ
            var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "/Nannnyandperant/良民證");// upload file path here
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);// check folder exist
            }

            var filePath = Path.Combine(uploadPath, model.photo1.FileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await model.photo1.CopyToAsync(fileStream);// write file into fileStream
              
            }
            var uploadPath2 = Path.Combine(_webHostEnvironment.WebRootPath, "/Nannnyandperant/保母證");// upload file path here
            if (!Directory.Exists(uploadPath2))
            {
                Directory.CreateDirectory(uploadPath2);// check folder exist
            }
            var filePath2 = Path.Combine(uploadPath2, model.photo2.FileName);
            using (var fileStream = new FileStream(filePath2, FileMode.Create))
            {
                await model.photo2.CopyToAsync(fileStream);// write file into fileStream
            }

            var uploadPath3 = Path.Combine(_webHostEnvironment.WebRootPath, "/Nannnyandperant/身分證");// upload file path here
            if (!Directory.Exists(uploadPath3))
            {
                Directory.CreateDirectory(uploadPath3);// check folder exist
            }
            var filePath3 = Path.Combine(uploadPath3, model.photo3.FileName);
            using (var fileStream = new FileStream(filePath3, FileMode.Create))
            {
                await model.photo3.CopyToAsync(fileStream);// write file into fileStream
            }
            // please let GroupBuyPhotoDTO complete
            Requirment.PoliceCriminalRecordCertificate = model.photo1.FileName;
            Requirment.ChildCareCertificate = model.photo2.FileName;
            Requirment.NationalIdentificationCard = model.photo3.FileName;
            Requirment.PoliceCriminalRecordCertificate = model.photoA;
            Requirment.ChildCareCertificate = model.photoB;
            Requirment.NationalIdentificationCard = model.photoC;
            _context.Update(Requirment);
            await _context.SaveChangesAsync();
             try
             {
                await _context.SaveChangesAsync();
               return RedirectToAction(nameof(Index));
              }
            catch (DbUpdateConcurrencyException)
            {
            if (!NannyRequirmentExists(model.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return View(model);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> DeletePhoto(int id)
    {
        var photo = await _context.NannyRequirments.FindAsync(id);
        if (photo == null)
        {
            return NotFound();
        }
        
        _context.NannyRequirments.Remove(photo);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Edit));
    }



    // GET: NannyRequirments/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var Requirment = (from nn in _context.NannyRequirments
                          where nn.Id == id
                          select new NannyRequirementDTO
                          {
                              Id = nn.Id,
                              //RequirementDate = nn.RequirementDate,
                              NannyAccountUserAccount = nn.NannyAccountUserAccount,
                              PoliceCriminalRecordCertificate = nn.PoliceCriminalRecordCertificate,
                              ChildCareCertificate = nn.ChildCareCertificate,
                              NationalIdentificationCard = nn.NationalIdentificationCard,
                              AddressesOfAgencies = nn.AddressesOfAgencies,
                              ValidPeriodsOfCertificates = nn.ValidPeriodsOfCertificates,
                              Statement = nn.Statement,
                              photoA = nn.PoliceCriminalRecordCertificate,
                              photoB = nn.ChildCareCertificate,
                              photoC = nn.NationalIdentificationCard,
                          }).FirstOrDefault();


    
        if (Requirment == null)
        {
            return NotFound();
        }

        return View(Requirment);
    }

    // POST: NannyRequirments/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int Id)
    {
        var nannyRequirment = await _context.NannyRequirments.FindAsync(Id);
        if (nannyRequirment != null)
        {
            _context.NannyRequirments.Remove(nannyRequirment);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool NannyRequirmentExists(int Id)
    {
        return _context.NannyRequirments.Any(e => e.Id == Id);
    }
}












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
//    public class NannyRequirmentsController : Controller
//    {
//        private readonly BabyCiaoContext _context;

//        public NannyRequirmentsController(BabyCiaoContext context)
//        {
//            _context = context;
//        }

//        // GET: NannyRequirments
//        public async Task<IActionResult> Index()
//        {
//            var babyCiaoContext = _context.NannyRequirments.Include(n => n.NannyAccountUserAccountNavigation);
//            return View(await babyCiaoContext.ToListAsync());
//        }

//        // GET: NannyRequirments/Details/5
//        public async Task<IActionResult> Details(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var nannyRequirment = await _context.NannyRequirments
//                .Include(n => n.NannyAccountUserAccountNavigation)
//                .FirstOrDefaultAsync(m => m.Id == id);
//            if (nannyRequirment == null)
//            {
//                return NotFound();
//            }

//            return View(nannyRequirment);
//        }

//        // GET: NannyRequirments/Create
//        public IActionResult Create()
//        {
//            ViewData["NannyAccountUserAccount"] = new SelectList(_context.UserAccounts, "Account", "Account");
//            return View();
//        }

//        // POST: NannyRequirments/Create
//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create([Bind("Id,RequirementDate,NannyAccountUserAccount,PoliceCriminalRecordCertificate,ChildCareCertificate,NationalIdentificationCard,AddressesOfAgencies,ValidPeriodsOfCertificates,Statement")] NannyRequirment nannyRequirment)
//        {
//            if (ModelState.IsValid)
//            {
//                _context.Add(nannyRequirment);
//                await _context.SaveChangesAsync();
//                return RedirectToAction(nameof(Index));
//            }
//            ViewData["NannyAccountUserAccount"] = new SelectList(_context.UserAccounts, "Account", "Account", nannyRequirment.NannyAccountUserAccount);
//            return View(nannyRequirment);
//        }

//        // GET: NannyRequirments/Edit/5
//        public async Task<IActionResult> Edit(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var nannyRequirment = await _context.NannyRequirments.FindAsync(id);
//            if (nannyRequirment == null)
//            {
//                return NotFound();
//            }
//            ViewData["NannyAccountUserAccount"] = new SelectList(_context.UserAccounts, "Account", "Account", nannyRequirment.NannyAccountUserAccount);
//            return View(nannyRequirment);
//        }

//        // POST: NannyRequirments/Edit/5
//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(int id, [Bind("Id,RequirementDate,NannyAccountUserAccount,PoliceCriminalRecordCertificate,ChildCareCertificate,NationalIdentificationCard,AddressesOfAgencies,ValidPeriodsOfCertificates,Statement")] NannyRequirment nannyRequirment)
//        {
//            if (id != nannyRequirment.Id)
//            {
//                return NotFound();
//            }

//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    _context.Update(nannyRequirment);
//                    await _context.SaveChangesAsync();
//                }
//                catch (DbUpdateConcurrencyException)
//                {
//                    if (!NannyRequirmentExists(nannyRequirment.Id))
//                    {
//                        return NotFound();
//                    }
//                    else
//                    {
//                        throw;
//                    }
//                }
//                return RedirectToAction(nameof(Index));
//            }
//            ViewData["NannyAccountUserAccount"] = new SelectList(_context.UserAccounts, "Account", "Account", nannyRequirment.NannyAccountUserAccount);
//            return View(nannyRequirment);
//        }

//        // GET: NannyRequirments/Delete/5
//        public async Task<IActionResult> Delete(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var nannyRequirment = await _context.NannyRequirments
//                .Include(n => n.NannyAccountUserAccountNavigation)
//                .FirstOrDefaultAsync(m => m.Id == id);
//            if (nannyRequirment == null)
//            {
//                return NotFound();
//            }

//            return View(nannyRequirment);
//        }

//        // POST: NannyRequirments/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteConfirmed(int id)
//        {
//            var nannyRequirment = await _context.NannyRequirments.FindAsync(id);
//            if (nannyRequirment != null)
//            {
//                _context.NannyRequirments.Remove(nannyRequirment);
//            }

//            await _context.SaveChangesAsync();
//            return RedirectToAction(nameof(Index));
//        }

//        private bool NannyRequirmentExists(int id)
//        {
//            return _context.NannyRequirments.Any(e => e.Id == id);
//        }
//    }
//}