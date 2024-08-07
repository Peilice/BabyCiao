//using BabyCiao.Models;
//using BabyCiao.Models.DTO;
//using BabyCiao.ViewModel;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace BabyCiao.Controllers
//{
//    public class NannyplatformController : Controller
//    {

//        private readonly BabyciaoContext _context;
//        private readonly IWebHostEnvironment _webHostEnvironment;

//        public NannyplatformController(BabyciaoContext context, IWebHostEnvironment webHostEnvironment)
//        {
//            _context = context;
//            _webHostEnvironment = webHostEnvironment;
//        }


//        public async Task<IActionResult> Nannyplatformindex()
//        {
//            var nannyResumes = await _context.NannyResume.ToListAsync();
//            var babyResumes = await _context.BabyResumes.ToListAsync();
//            var userInformations = await _context.UserInformation.ToListAsync();

//            var viewModel = nannyResumes.Select(nannyResume => new NannyplatformViewModel
//            {
//                NannyResume = nannyResume,
//                BabyResume = babyResumes.FirstOrDefault(br => br.AccountUserAccount == nannyResume.NannyAccountUserAccount),
//                UserInformation = userInformations.FirstOrDefault(ui => ui.AccountUser == nannyResume.NannyAccountUserAccount)
//            }).ToList();

//            return View(viewModel);
//        }



//        public async Task<IActionResult> Nannyindex()
//        {
//            var nannyResumes = await _context.NannyResume.ToListAsync();
//            var babyResumes = await _context.BabyResumes.ToListAsync();
//            var userInformations = await _context.UserInformation.ToListAsync();

//            var viewModel = nannyResumes.Select(nannyResume => new NannyplatformViewModel
//            {
//                NannyResume = nannyResume,
//                BabyResume = babyResumes.FirstOrDefault(br => br.AccountUserAccount == nannyResume.NannyAccountUserAccount),
//                UserInformation = userInformations.FirstOrDefault(ui => ui.AccountUser == nannyResume.NannyAccountUserAccount)
//            });

//            return View(viewModel);
//        }


//        public async Task<IActionResult> babyindex()
//        {
//            var nannyResumes = await _context.NannyResume.ToListAsync();
//            var babyResumes = await _context.BabyResumes.ToListAsync();
//            var userInformations = await _context.UserInformation.ToListAsync();

//            var viewModel = nannyResumes.Select(nannyResume => new NannyplatformViewModel
//            {
//                NannyResume = nannyResume,
//                BabyResume = babyResumes.FirstOrDefault(br => br.AccountUserAccount == nannyResume.NannyAccountUserAccount),
//                UserInformation = userInformations.FirstOrDefault(ui => ui.AccountUser == nannyResume.NannyAccountUserAccount)
//            }).ToList();

//            return View(viewModel);
//        }



//        public async Task<IActionResult> NannyDetails(int id, BabyciaoContext _context)
//        {
//            if (id == 0)
//            {
//                return NotFound();
//            }

//            var resumeDetail = await (from nd in _context.NannyResume
//                                      where nd.id == id
//                                      select new NannyplatformViewModel
//                                      {
//                                          NannyResume = nd,
//                                          UserInformation = _context.UserInformation.FirstOrDefault(ui => ui.AccountUser == nd.NannyAccountUserAccount),
//                                          BabyResume = _context.BabyResumes.FirstOrDefault(br => br.AccountUserAccount == nd.NannyAccountUserAccount)
//                                      }).FirstOrDefaultAsync();

//            if (resumeDetail == null)
//            {
//                return NotFound();
//            }

//            return View(resumeDetail);
//        }


//        public async Task<IActionResult> BabyDetails(int id)
//        {
//            if (id == 0)
//            {
//                return NotFound();
//            }

//            var resumeDetail = await (from bb in _context.BabyResumes
//                                      where bb.Id == id
//                                      select new NannyplatformViewModel
//                                      {
//                                          BabyResume = bb,
//                                          UserInformation = _context.UserInformation.FirstOrDefault(ui => ui.AccountUser == bb.AccountUserAccount),
//                                          NannyResume = _context.NannyResume.FirstOrDefault(nr => nr.NannyAccountUserAccount == bb.AccountUserAccount)
//                                      }).FirstOrDefaultAsync();

//            if (resumeDetail == null)
//            {
//                return NotFound();
//            }

//            return View(resumeDetail);
//        }


//        [HttpGet]
//        public async Task<IActionResult> EditNanny(int id)
//        {
//            if (id == 0)
//            {
//                return NotFound();
//            }

//            var nanny = await _context.NannyResume.FindAsync(id);
//            if (nanny == null)
//            {
//                return NotFound();
//            }

//            var viewModel = new NannyplatformViewModel
//            {
//                NannyResume = nanny,
//                UserInformation = await _context.UserInformation.FirstOrDefaultAsync(ui => ui.AccountUser == nanny.NannyAccountUserAccount)
//            };

//            return View(viewModel);
//        }


//        [HttpPost]
//        public async Task<IActionResult> EditNanny(NannyplatformViewModel viewModel)
//        {
//            if (ModelState.IsValid)
//            {
//                var nanny = await _context.NannyResume.FindAsync(viewModel.NannyResume.id);
//                if (nanny == null)
//                {
//                    return NotFound();
//                }

//                nanny.NannyAccountUserAccount = viewModel.NannyResume.NannyAccountUserAccount;
//                nanny.City = viewModel.NannyResume.City;
//                nanny.District = viewModel.NannyResume.District;
//                nanny.QuasiPublicChildcare = viewModel.NannyResume.QuasiPublicChildcare;
//                nanny.Introduction = viewModel.NannyResume.Introduction;
//                nanny.TypeOfDaycare = viewModel.NannyResume.TypeOfDaycare;
//                nanny.ServiceItems = viewModel.NannyResume.ServiceItems;
//                nanny.ChildcareAvailableUnder2 = viewModel.NannyResume.ChildcareAvailableUnder2;
//                nanny.ChildcareAvailableOver2 = viewModel.NannyResume.ChildcareAvailableOver2;
//                nanny.Language = viewModel.NannyResume.Language;
//                nanny.ServiceCenter = viewModel.NannyResume.ServiceCenter;
//                nanny.ProfessionalPortrait = viewModel.NannyResume.ProfessionalPortrait;
//                nanny.DisplayControl = viewModel.NannyResume.DisplayControl;

//                _context.Update(nanny);
//                await _context.SaveChangesAsync();

//                return RedirectToAction(nameof(NannyDetails), new { id = viewModel.NannyResume.id });
//            }

//            return View(viewModel);
//        }


//        [HttpGet]
//        public async Task<IActionResult> EditBaby(int id)
//        {
//            if (id == 0)
//            {
//                return NotFound();
//            }

//            var baby = await _context.BabyResumes.FindAsync(id);
//            if (baby == null)
//            {
//                return NotFound();
//            }

//            var viewModel = new NannyplatformViewModel
//            {
//                BabyResume = baby,
//                UserInformation = await _context.UserInformation.FirstOrDefaultAsync(ui => ui.AccountUser == baby.AccountUserAccount)
//            };

//            return View(viewModel);
//        }


//        [HttpPost]
//        public async Task<IActionResult> EditBaby(NannyplatformViewModel viewModel)
//        {
//            if (ModelState.IsValid)
//            {
//                var baby = await _context.BabyResumes.FindAsync(viewModel.BabyResume.Id);
//                if (baby == null)
//                {
//                    return NotFound();
//                }

//                baby.AccountUserAccount = viewModel.BabyResume.AccountUserAccount;
//                baby.Photo = viewModel.BabyResume.Photo;
//                baby.FirstName = viewModel.BabyResume.FirstName;
//                baby.City = viewModel.BabyResume.City;
//                baby.District = viewModel.BabyResume.District;
//                baby.ApplyDate = viewModel.BabyResume.ApplyDate;
//                baby.RequireDate = viewModel.BabyResume.RequireDate;
//                baby.Babyage = viewModel.BabyResume.Babyage;
//                baby.TypeOfDaycare = viewModel.BabyResume.TypeOfDaycare;
//                baby.TimeSlot = viewModel.BabyResume.TimeSlot;
//                baby.Memo = viewModel.BabyResume.Memo;
//                baby.Display = viewModel.BabyResume.Display;

//                _context.Update(baby);
//                await _context.SaveChangesAsync();

//                return RedirectToAction(nameof(BabyDetails), new { id = viewModel.BabyResume.Id });
//            }

//            return View(viewModel);
//        }


//    }
//}