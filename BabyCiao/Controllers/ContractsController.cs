//using BabyCiao.Data;
//using BabyCiao.Models;
//using BabyCiao.Models.DTO;
//using BabyCiao.ViewModel;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Threading.Tasks;

//namespace BabyCiao.Controllers
//{
//    public class ContractsController : Controller
//    {
//            private readonly BabyciaoContext _context;
//            private readonly IWebHostEnvironment _webHostEnvironment;

//            public ContractsController(BabyciaoContext context, IWebHostEnvironment webHostEnvironment)
//            {
//                _context = context;
//                _webHostEnvironment = webHostEnvironment;
//            }
//        private static readonly Dictionary<int, string> ContractStatementDictionary = new Dictionary<int, string>
//          {
//            { 0, "審核中" },
//            { 1, "審核成功" },
//            { 2, "待補件" },
//            { 3, "取消合約" },
//            { 4, "終止合約"}
//           };

//        // GET: Contracts
//        public async Task<IActionResult> Index()
//        {
//            var contracts = await _context.Contracts
//                .Select(c => new ContractViewModel
//                {
//                    ContractId = c.ContractId,
//                    NannyAccountUserAccount = c.NannyAccountUserAccount,
//                    NannySignature = c.NannySignature,
//                    AccountUserAccount = c.AccountUserAccount,
//                    UserSignature = c.UserSignature,
//                    ContractStartTime = c.ContractStartTime,
//                    ContractFinishTime = c.ContractFinishTime,
//                    ContractFile = c.ContractFile,
//                    Statement = c.Statement,
//                    ModifiedTime = c.ModifiedTime,
//                    BuiledTime = c.BuiledTime,
//                    Display = c.Display,
//                    AccountUserAccountNavigation = c.AccountUserAccountNavigation,
//                    NannyAccountUserAccountNavigation = c.NannyAccountUserAccountNavigation
//                }).ToListAsync();

//            return View(contracts);
//        }



//        //public async Task<IActionResult> Create([FromForm] ContractViewModel model)
//        //{
//        //    if (model == null)
//        //    {
//        //        return NotFound();
//        //    }

//        //    var contract = new ContractViewModel
//        //    {
//        //        ContractId = model.ContractId,
//        //        NannyAccountUserAccount = model.NannyAccountUserAccount,
//        //        NannySignature = model.NannySignature,
//        //        AccountUserAccount = model.AccountUserAccount,
//        //        UserSignature = model.UserSignature,
//        //        ContractStartTime = model.ContractStartTime,
//        //        ContractFinishTime = model.ContractFinishTime,
//        //        ContractFile = model.ContractFile,
//        //        Statement = model.Statement,
//        //        ModifiedTime = model.ModifiedTime,
//        //        BuiledTime = model.BuiledTime,
//        //        Display = model.Display,
//        //        NannySignatureFile = model.NannySignatureFile,
//        //        UserSignatureFile = model.UserSignatureFile,
//        //        ContractFilePath = model.ContractFilePath,
//        //    };

//        //    if (model.NannyAccountUserAccount != null && model.ContractFile != null && model.UploadedNannySignatureFile != null && model.UploadedUserSignatureFile != null)
//        //    {
//        //        var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "NannnyAndPerant/合約");
//        //        var uploadPath2 = Path.Combine(_webHostEnvironment.WebRootPath, "NannnyAndPerant/保母簽名檔");
//        //        var uploadPath3 = Path.Combine(_webHostEnvironment.WebRootPath, "NannnyAndPerant/家長簽名檔");

//        //        if (!Directory.Exists(uploadPath)) Directory.CreateDirectory(uploadPath);
//        //        if (!Directory.Exists(uploadPath2)) Directory.CreateDirectory(uploadPath2);
//        //        if (!Directory.Exists(uploadPath3)) Directory.CreateDirectory(uploadPath3);

//        //        var contractFilePath = Path.Combine(uploadPath, model.ContractFile.FileName);
//        //        var nannySignatureFilePath = Path.Combine(uploadPath2, model.UploadedNannySignatureFile.FileName);
//        //        var userSignatureFilePath = Path.Combine(uploadPath3, model.UploadedUserSignatureFile.FileName);

//        //        using (var fileStream = new FileStream(contractFilePath, FileMode.Create))
//        //        {
//        //            await model.ContractFile.CopyToAsync(fileStream);
//        //        }
//        //        using (var fileStream = new FileStream(nannySignatureFilePath, FileMode.Create))
//        //        {
//        //            await model.UploadedNannySignatureFile.CopyToAsync(fileStream);
//        //        }
//        //        using (var fileStream = new FileStream(userSignatureFilePath, FileMode.Create))
//        //        {
//        //            await model.UploadedUserSignatureFile.CopyToAsync(fileStream);
//        //        }

//        //        contract.ContractFile = model.ContractFile.FileName;
//        //        contract.NannySignatureFile = model.UploadedNannySignatureFile.FileName;
//        //        contract.UserSignatureFile = model.UploadedUserSignatureFile.FileName;

//        //        _context.Add(contract);
//        //        await _context.SaveChangesAsync();
//        //    }
//        //    return RedirectToAction(nameof(Index));
//        //}

//        // POST: NannyRequirments/Create
//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        //[HttpPost]
//        //[ValidateAntiForgeryToken]
//        //public async Task<IActionResult> Create([FromForm] ContractViewModel model)
//        //{
//        //    if (model == null)
//        //    {
//        //        return NotFound();
//        //    }


//        //    var Contract = new ContractViewModel
//        //    {
//        //        ContractId = model.ContractId,
//        //        NannyAccountUserAccount = model.NannyAccountUserAccount,
//        //        NannySignature = model.NannySignature,
//        //        AccountUserAccount = model.AccountUserAccount,
//        //        UserSignature = model.UserSignature,
//        //        ContractStartTime = model.ContractStartTime,
//        //        ContractFinishTime = model.ContractFinishTime,
//        //        ContractFile = model.ContractFile,
//        //        Statement = model.Statement,
//        //        ModifiedTime = model.ModifiedTime,
//        //        BuiledTime = model.BuiledTime,
//        //        Display = model.Display,
//        //        NannySignatureFile = model.NannySignatureFile,
//        //        UserSignatureFile= model.UserSignatureFile,
//        //        ContractFilePath= model.ContractFilePath,
//        //    };

//        //    if (model.NannyAccountUserAccount != null && model.ContractFile != null && model.NannySignatureFile != null && model.UserSignatureFile !=null)
//        //    {
//        //        //這裡處理檔案寫入資料庫的處理
//        //        var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "NannnyAndPerant/合約");// upload file path here
//        //        var uploadPath2 = Path.Combine(_webHostEnvironment.WebRootPath, "NannnyAndPerant/保母電子簽名檔");// upload file path here
//        //        var uploadPath3 = Path.Combine(_webHostEnvironment.WebRootPath, "NannnyAndPerant/家長電子簽名檔");// upload file path here

//        //        if (!Directory.Exists(uploadPath))
//        //        {
//        //            Directory.CreateDirectory(uploadPath);// check folder exist
//        //        }

//        //        var filePath = Path.Combine(uploadPath, model.UploadedContractFile.FileName);
//        //        using (var fileStream = new FileStream(filePath, FileMode.Create))
//        //        {
//        //            await model.UploadedContractFile.CopyToAsync(fileStream);// write file into fileStream
//        //        }
//        //        var filePath2 = Path.Combine(uploadPath2, model.UploadedNannySignatureFile.FileName);
//        //        using (var fileStream = new FileStream(filePath, FileMode.Create))
//        //        {
//        //            await model.UploadedNannySignatureFile.CopyToAsync(fileStream);// write file into fileStream
//        //        }
//        //        var filePath3 = Path.Combine(uploadPath3, model.UploadedUserSignatureFile.FileName);
//        //        using (var fileStream = new FileStream(filePath, FileMode.Create))
//        //        {
//        //            await model.UploadedUserSignatureFile.CopyToAsync(fileStream);// write file into fileStream
//        //        }
//        //        // please let GroupBuyPhotoDTO complete
//        //        Contract.ContractFile = model.UploadedContractFile.FileName;
//        //        Contract.NannySignatureFile = model.UploadedNannySignatureFile.FileName;
//        //        Contract.UserSignatureFile = model.UploadedUserSignatureFile.FileName;
//        //        _context.Add(Contract);

//        //        Contract.ContractFile = model.ContractFilePath;
//        //        Contract.NannySignatureFile = model.NannySignatureFilePath;
//        //        Contract.UserSignatureFile = model.UserSignatureFilePath;

//        //        await _context.SaveChangesAsync();
//        //    }
//        //    return RedirectToAction(nameof(Index));
//        //}



//        public async Task<IActionResult> Edit(int id)
//        {
//            var contract = await _context.Contracts
//                .Select(c => new ContractViewModel
//                {
//                    ContractId = c.ContractId,
//                    NannyAccountUserAccount = c.NannyAccountUserAccount,
//                    NannySignature = c.NannySignature,
//                    AccountUserAccount = c.AccountUserAccount,
//                    UserSignature = c.UserSignature,
//                    ContractStartTime = c.ContractStartTime,
//                    ContractFinishTime = c.ContractFinishTime,
//                    ContractFile = c.ContractFile,
//                    Statement = c.Statement,
//                    ModifiedTime = c.ModifiedTime,
//                    BuiledTime = c.BuiledTime,
//                    Display = c.Display,
//                    UserSignatureFile= c.UserSignatureFile,
//                    NannySignatureFile= c.NannySignatureFile,
//                    ContractFilePath = $"/NannnyAndPerant/合約/{c.ContractFile}.pdf",
//                    NannySignatureFilePath = $"/NannnyAndPerant/保母電子簽名檔/{c.NannySignatureFile}",
//                    UserSignatureFilePath = $"/NannnyAndPerant/家長電子簽名檔/{c.UserSignatureFile}"
//                })
//                .FirstOrDefaultAsync(m => m.ContractId == id);

//            //if (contract.ContractFilePath != null)
//            //{
//            //    var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "NannnyAndPerant/合約", contract.ContractFile);
//            //    using (var stream = new FileStream(filePath, FileMode.Create))
//            //    {
//            //        await contract.ContractFile1.CopyToAsync(stream);
//            //    }
//            //    contract.ContractFile = $"/NannnyAndPerant/合約/{contract.ContractFile}";
//            //}

//            ////if (contract.NannySignatureFile != null)
//            //{
//            //    var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "NannnyAndPerant/保母簽名檔", contract.NannySignatureFile);
//            //    using (var stream = new FileStream(filePath, FileMode.Create))
//            //    {
//            //        await contract.UploadedNannySignatureFile1.CopyToAsync(stream);
//            //    }
//            //    contract.NannySignatureFilePath = $"/NannnyAndPerant/保母簽名檔/{contract.NannySignatureFile}";
//            //}

//            //if (contract.UserSignatureFile != null)
//            //{
//            //    var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "NannnyAndPerant/家長簽名檔", contract.UserSignatureFile);
//            //    using (var stream = new FileStream(filePath, FileMode.Create))
//            //    {
//            //        await contract.UploadedUserSignatureFile1.CopyToAsync(stream);
//            //    }
//            //    contract.UserSignatureFilePath = $"/NannnyAndPerant/家長簽名檔/{contract.UploadedUserSignatureFile1}";
//            //}


//            if (contract == null)
//            {
//                return NotFound();
//            }

//            return View(contract);
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(int id, ContractViewModel model)
//        {
//            if (id != model.ContractId)
//            {
//                return NotFound();
//            }

//            if (ModelState.IsValid)
//            {
//                var contract = await _context.Contracts.FindAsync(id);
//                if (contract == null)
//                {
//                    return NotFound();
//                }

//                contract.NannyAccountUserAccount = model.NannyAccountUserAccount;
//                contract.NannySignature = model.NannySignature;
//                contract.AccountUserAccount = model.AccountUserAccount;
//                contract.UserSignature = model.UserSignature;
//                contract.ContractStartTime = model.ContractStartTime;
//                contract.ContractFinishTime = model.ContractFinishTime;
//                contract.Statement = model.Statement;
//                contract.ModifiedTime = DateTime.Now;
//                contract.Display = model.Display;
//                contract.NannySignatureFile = model.NannySignatureFile;
//                contract.UserSignatureFile = model.UserSignatureFile;

//                _context.Update(contract);
//                await _context.SaveChangesAsync();
//                return RedirectToAction(nameof(Index));
//            }
//            return View(model);
//        }

//        // POST: Contracts/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteConfirmed(int id)
//        {
//            var contract = await _context.Contracts.FindAsync(id);
//            if (contract != null)
//            {
//                _context.Contracts.Remove(contract);
//                await _context.SaveChangesAsync();
//            }
//            return RedirectToAction(nameof(Index));
//        }
    





//    //[HttpPost]
//    //[ValidateAntiForgeryToken]
//    //public async Task<IActionResult> Edit(int id, ContractViewModel model)
//    //{
//    //    if (id != model.ContractId)
//    //    {
//    //        return NotFound();
//    //    }

//    //    if (ModelState.IsValid)
//    //    {
//    //        var contract = _context.Contracts.Find(id);
//    //        if (contract == null)
//    //        {
//    //            return NotFound();
//    //        }

//    //        // 更新合约信息
//    //        contract.NannyAccountUserAccount = model.NannyAccountUserAccount;
//    //        contract.NannySignature = model.NannySignature;
//    //        contract.AccountUserAccount = model.AccountUserAccount;
//    //        contract.UserSignature = model.UserSignature;
//    //        contract.ContractStartTime = model.ContractStartTime;
//    //        contract.ContractFinishTime = model.ContractFinishTime;
//    //        contract.Statement = model.Statement;
//    //        contract.ModifiedTime = DateTime.Now;
//    //        contract.Display = model.Display;

    //    if (ModelState.IsValid)
    //    {
    //        var contract = _context.Contracts.Find(id);
    //        if (contract == null)
    //        {
    //            return NotFound();
    //        }

//    //        if (model.UploadedContractFile != null)
//    //        {
//    //            var filePath = Path.Combine("wwwroot/NannnyAndPerant/合約", model.UploadedContractFile.FileName);
//    //            using (var stream = new FileStream(filePath, FileMode.Create))
//    //            {
//    //                await model.UploadedContractFile.CopyToAsync(stream);
//    //            }
//    //            contract.ContractFile = model.UploadedContractFile.FileName;
//    //        }

//    //        if (model.UploadedNannySignatureFile != null)
//    //        {
//    //            var filePath = Path.Combine("wwwroot/NannnyAndPerant/保母簽名檔", model.NannySignatureFile);
//    //            using (var stream = new FileStream(filePath, FileMode.Create))
//    //            {
//    //                await model.UploadedNannySignatureFile.CopyToAsync(stream);
//    //            }
//    //        }

//    //        if (model.UploadedUserSignatureFile != null)
//    //        {
//    //            var filePath = Path.Combine("wwwroot/NannnyAndPerant/家長簽名檔", model.UserSignatureFile);
//    //            using (var stream = new FileStream(filePath, FileMode.Create))
//    //            {
//    //                await model.UploadedUserSignatureFile.CopyToAsync(stream);
//    //            }
//    //        }

//    //        // 保存更改
//    //        _context.Update(contract);
//    //        await _context.SaveChangesAsync();
//    //        return RedirectToAction(nameof(Index));
//    //    }
//    //    return View(model);
//    //}

//    // 退件功能
//    public async Task<IActionResult> ReturnContract(int id)
//        {
//            var contract = await _context.Contracts.FindAsync(id);
//            if (contract == null)
//            {
//                return NotFound();
//            }
    //        }

    //        // 保存更改
    //        _context.Update(contract);
    //        await _context.SaveChangesAsync();
    //        return RedirectToAction(nameof(Index));
    //    }
    //    return View(model);
    //}

//            // 执行退件逻辑，如更改状态等
//            contract.Statement = "Returned";  // 假设有个 Status 字段表示合约状态
//            await _context.SaveChangesAsync();

//            // 向用户发送通知（可以使用邮件、站内信等方式）

//            return RedirectToAction(nameof(Index));
//        }
//        }

        //    return RedirectToAction(nameof(Index));
        //}
        //}

















//        // GET: Contracts/Details/5
//        //public async Task<IActionResult> Details(int? id)
//        //{
//        //    if (id == null)
//        //    {
//        //        return NotFound();
//        //    }

//        //    var contract = await _context.Contracts
//        //        .Select(c => new ContractViewModel
//        //        {
//        //            ContractId = c.ContractId,
//        //            NannyAccountUserAccount = c.NannyAccountUserAccount,
//        //            NannySignature = c.NannySignature,
//        //            AccountUserAccount = c.AccountUserAccount,
//        //            UserSignature = c.UserSignature,
//        //            ContractStartTime = c.ContractStartTime,
//        //            ContractFinishTime = c.ContractFinishTime,
//        //            ContractFile = c.ContractFile,
//        //            Statement = c.Statement,
//        //            ModifiedTime = c.ModifiedTime,
//        //            BuiledTime = c.BuiledTime,
//        //            Display = c.Display,
//        //            AccountUserAccountNavigation = c.AccountUserAccountNavigation,
//        //            NannyAccountUserAccountNavigation = c.NannyAccountUserAccountNavigation
//        //        })
//        //        .FirstOrDefaultAsync(m => m.ContractId == id);

//        //    if (contract == null)
//        //    {
//        //        return NotFound();
//        //    }

//        //    return View(contract);
//        //}

//        //// GET: Contracts/Create
//        //public IActionResult Create()
//        //{
//        //    return View();
//        //}

//        //// POST: Contracts/Create
//        //[HttpPost]
//        //[ValidateAntiForgeryToken]
//        //public async Task<IActionResult> Create([Bind("ContractId,NannyAccountUserAccount,NannySignature,AccountUserAccount,UserSignature,ContractStartTime,ContractFinishTime,ContractFile,Statement,ModifiedTime,BuiledTime,Display")] ContractViewModel contractViewModel)
//        //{
//        //    if (ModelState.IsValid)
//        //    {
//        //        var contract = new Contract
//        //        {
//        //            ContractId = contractViewModel.ContractId,
//        //            NannyAccountUserAccount = contractViewModel.NannyAccountUserAccount,
//        //            NannySignature = contractViewModel.NannySignature,
//        //            AccountUserAccount = contractViewModel.AccountUserAccount,
//        //            UserSignature = contractViewModel.UserSignature,
//        //            ContractStartTime = contractViewModel.ContractStartTime,
//        //            ContractFinishTime = contractViewModel.ContractFinishTime,
//        //            ContractFile = contractViewModel.ContractFile,
//        //            Statement = contractViewModel.Statement,
//        //            ModifiedTime = DateTime.Now,
//        //            BuiledTime = DateTime.Now,
//        //            Display = contractViewModel.Display
//        //        };

//        //        _context.Add(contract);
//        //        await _context.SaveChangesAsync();
//        //        return RedirectToAction(nameof(Index));
//        //    }
//        //    return View(contractViewModel);
//        //}

        //        _context.Add(contract);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(contractViewModel);
        //}


//        //    // GET: Contracts/Edit/5
//        //    public IActionResult Edit(int id)
//        //    {
//        //        var contract = _context.Contracts.Find(id);
//        //        if (contract == null)
//        //        {
//        //            return NotFound();
//        //        }

//        //        var viewModel = new ContractViewModel
//        //        {
//        //            ContractId = contract.ContractId,
//        //            NannyAccountUserAccount = contract.NannyAccountUserAccount,
//        //            NannySignature = contract.NannySignature,
//        //            AccountUserAccount = contract.AccountUserAccount,
//        //            UserSignature = contract.UserSignature,
//        //            ContractStartTime = contract.ContractStartTime,
//        //            ContractFinishTime = contract.ContractFinishTime,
//        //            ContractFile = contract.ContractFile,
//        //            Statement = contract.Statement,
//        //            ModifiedTime = contract.ModifiedTime,
//        //            BuiledTime = contract.BuiledTime,
//        //            Display = contract.Display,
//        //            NannySignatureFile = $"~/NannyAndParent/保母電子簽名檔/{contract.NannySignatureFile}",
//        //            UserSignatureFile = $"~/NannyAndParent/家長電子簽名檔/{contract.UserSignatureFile}"
//        //        };

//        //        return View(viewModel);
//        //    }

//        //// POST: Contracts/Edit/5
//        //[HttpPost]
//        //    [ValidateAntiForgeryToken]
//        //    public async Task<IActionResult> Edit(int id, [Bind("ContractId,NannyAccountUserAccount,NannySignature,AccountUserAccount,UserSignature,ContractStartTime,ContractFinishTime,ContractFile,Statement,ModifiedTime,BuiledTime,Display")] ContractViewModel contractViewModel)
//        //    {
//        //        if (id != contractViewModel.ContractId)
//        //        {
//        //            return NotFound();
//        //        }

//        //        if (ModelState.IsValid)
//        //        {
//        //            try
//        //            {
//        //                var contract = await _context.Contracts.FindAsync(id);
//        //                if (contract == null)
//        //                {
//        //                    return NotFound();
//        //                }

//        //                contract.NannyAccountUserAccount = contractViewModel.NannyAccountUserAccount;
//        //                contract.NannySignature = contractViewModel.NannySignature;
//        //                contract.AccountUserAccount = contractViewModel.AccountUserAccount;
//        //                contract.UserSignature = contractViewModel.UserSignature;
//        //                contract.ContractStartTime = contractViewModel.ContractStartTime;
//        //                contract.ContractFinishTime = contractViewModel.ContractFinishTime;
//        //                contract.ContractFile = contractViewModel.ContractFile;
//        //                contract.Statement = contractViewModel.Statement;
//        //                contract.ModifiedTime = DateTime.Now;
//        //                contract.BuiledTime = contractViewModel.BuiledTime;
//        //                contract.Display = contractViewModel.Display;

//        //                _context.Update(contract);
//        //                await _context.SaveChangesAsync();
//        //            }
//        //            catch (DbUpdateConcurrencyException)
//        //            {
//        //                if (!ContractExists(contractViewModel.ContractId))
//        //                {
//        //                    return NotFound();
//        //                }
//        //                else
//        //                {
//        //                    throw;
//        //                }
//        //            }
//        //            return RedirectToAction(nameof(Index));
//        //        }
//        //        return View(contractViewModel);
//        //    }

//        //    // GET: Contracts/Delete/5
//        //    public async Task<IActionResult> Delete(int? id)
//        //    {
//        //        if (id == null)
//        //        {
//        //            return NotFound();
//        //        }

//        //        var contract = await _context.Contracts
//        //            .FirstOrDefaultAsync(m => m.ContractId == id);

//        //        if (contract == null)
//        //        {
//        //            return NotFound();
//        //        }

//        //        var contractViewModel = new ContractViewModel
//        //        {
//        //            ContractId = contract.ContractId,
//        //            NannyAccountUserAccount = contract.NannyAccountUserAccount,
//        //            NannySignature = contract.NannySignature,
//        //            AccountUserAccount = contract.AccountUserAccount,
//        //            UserSignature = contract.UserSignature,
//        //            ContractStartTime = contract.ContractStartTime,
//        //            ContractFinishTime = contract.ContractFinishTime,
//        //            ContractFile = contract.ContractFile,
//        //            Statement = contract.Statement,
//        //            ModifiedTime = contract.ModifiedTime,
//        //            BuiledTime = contract.BuiledTime,
//        //            Display = contract.Display,
//        //            AccountUserAccountNavigation = contract.AccountUserAccountNavigation,
//        //            NannyAccountUserAccountNavigation = contract.NannyAccountUserAccountNavigation
//        //        };

//        //        return View(contractViewModel);
//        //    }

//        //    // POST: Contracts/Delete/5
//        //    [HttpPost, ActionName("Delete")]
//        //    [ValidateAntiForgeryToken]
//        //    public async Task<IActionResult> DeleteConfirmed(int id)
//        //    {
//        //        var contract = await _context.Contracts.FindAsync(id);
//        //        if (contract != null)
//        //        {
//        //            _context.Contracts.Remove(contract);
//        //            await _context.SaveChangesAsync();
//        //        }
//        //        return RedirectToAction(nameof(Index));
//        //    }

//        //private bool ContractExists(int id)
//        //{
//        //    return _context.Contracts.Any(e => e.ContractId == id);
//        //}
//    }

        //private bool ContractExists(int id)
        //{
        //    return _context.Contracts.Any(e => e.ContractId == id);
        //}
    

