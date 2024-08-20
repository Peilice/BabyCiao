using BabyCiao.Models;
using BabyCiao.Models.DTO;
using BabyCiao.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace BabyCiao.Controllers
{
    public class NannyplatformController : Controller
    {
        private readonly BabyciaoContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public NannyplatformController(BabyciaoContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Nannyplatformindex()
        {
            var nannyResumes = await _context.NannyResumes.ToListAsync();
            var babyResumes = await _context.BabyResumes.ToListAsync();
            var userInformations = await _context.UserInformations.ToListAsync();
            var nannyrequirments = await _context.NannyRequirments.ToListAsync();

            var viewModel = nannyResumes.Select(nannyResume => new NannyplatformViewModel
            {
                NannyResume = nannyResume,
                BabyResume = babyResumes.FirstOrDefault(br => br.AccountUserAccount == nannyResume.NannyAccountUserAccount) ?? new BabyResume(),
                UserInformation = userInformations.FirstOrDefault(ui => ui.AccountUser == nannyResume.NannyAccountUserAccount) ?? new UserInformation()
              
            }).ToList();

            return View(viewModel);
        }

        public async Task<IActionResult> Nannyindex()
        {
            var nannyResumes = await _context.NannyResumes.ToListAsync();
            var babyResumes = await _context.BabyResumes.ToListAsync();
            var userInformations = await _context.UserInformations.ToListAsync();

            var viewModel = nannyResumes.Select(nannyResume => new NannyplatformViewModel
            {
                NannyResume = nannyResume,
                BabyResume = babyResumes.FirstOrDefault(br => br.AccountUserAccount == nannyResume.NannyAccountUserAccount) ?? new BabyResume(),
                UserInformation = userInformations.FirstOrDefault(ui => ui.AccountUser == nannyResume.NannyAccountUserAccount) ?? new UserInformation()
            }).ToList();

            return View(viewModel);
        }

        public async Task<IActionResult> babyindex()
        {
            var nannyResumes = await _context.NannyResumes.ToListAsync();
            var babyResumes = await _context.BabyResumes.ToListAsync();
            var userInformations = await _context.UserInformations.ToListAsync();

            var viewModel = nannyResumes.Select(nannyResume => new NannyplatformViewModel
            {
                NannyResume = nannyResume,
                BabyResume = babyResumes.FirstOrDefault(br => br.AccountUserAccount == nannyResume.NannyAccountUserAccount) ?? new BabyResume(),
                UserInformation = userInformations.FirstOrDefault(ui => ui.AccountUser == nannyResume.NannyAccountUserAccount) ?? new UserInformation()
            }).ToList();

            return View(viewModel);
        }

        public async Task<IActionResult> NannyDetails(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var nannyResume = await _context.NannyResumes.FindAsync(id);
            if (nannyResume == null)
            {
                return NotFound();
            }

            var userInformation = await _context.UserInformations
                                                .FirstOrDefaultAsync(ui => ui.AccountUser == nannyResume.NannyAccountUserAccount);
            var babyResume = await _context.BabyResumes
                                            .FirstOrDefaultAsync(br => br.AccountUserAccount == nannyResume.NannyAccountUserAccount);

            var resumeDetail = new NannyplatformViewModel
            {
                NannyResume = nannyResume,
                UserInformation = userInformation ?? new UserInformation(),
                BabyResume = babyResume ?? new BabyResume()
            };

            return View(resumeDetail);
        }

        public async Task<IActionResult> BabyDetails(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var babyResume = await _context.BabyResumes.FindAsync(id);
            if (babyResume == null)
            {
                return NotFound();
            }

            var userInformation = await _context.UserInformations
                                                .FirstOrDefaultAsync(ui => ui.AccountUser == babyResume.AccountUserAccount);
            var nannyResume = await _context.NannyResumes
                                             .FirstOrDefaultAsync(nr => nr.NannyAccountUserAccount == babyResume.AccountUserAccount);

            var resumeDetail = new NannyplatformViewModel
            {
                BabyResume = babyResume,
                UserInformation = userInformation ?? new UserInformation(),
                NannyResume = nannyResume ?? new NannyResume()
            };

            return View(resumeDetail);
        }

        [HttpGet]
        public async Task<IActionResult> EditNanny(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var nanny = await _context.NannyResumes.FindAsync(id);
            if (nanny == null)
            {
                return NotFound();
            }

            var userInformation = await _context.UserInformations
                                                .FirstOrDefaultAsync(ui => ui.AccountUser == nanny.NannyAccountUserAccount);

            var viewModel = new NannyplatformViewModel
            {
                NannyResume = nanny,
                UserInformation = userInformation ?? new UserInformation()
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditNanny(NannyplatformViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var nanny = await _context.NannyResumes.FindAsync(viewModel.NannyResume.Id);
                if (nanny == null)
                {
                    return NotFound();
                }

                nanny.NannyAccountUserAccount = viewModel.NannyResume.NannyAccountUserAccount;
                nanny.City = viewModel.NannyResume.City;
                nanny.District = viewModel.NannyResume.District;
                nanny.QuasiPublicChildcare = viewModel.NannyResume.QuasiPublicChildcare;
                nanny.Introduction = viewModel.NannyResume.Introduction;
                nanny.TypeOfDaycare = viewModel.NannyResume.TypeOfDaycare;
                nanny.ServiceItems = viewModel.NannyResume.ServiceItems;
                nanny.ChildcareAvailableUnder2 = viewModel.NannyResume.ChildcareAvailableUnder2;
                nanny.ChildcareAvailableOver2 = viewModel.NannyResume.ChildcareAvailableOver2;
                nanny.Language = viewModel.NannyResume.Language;
                nanny.ServiceCenter = viewModel.NannyResume.ServiceCenter;
                nanny.ProfessionalPortrait = viewModel.NannyResume.ProfessionalPortrait;
                nanny.DisplayControl = viewModel.NannyResume.DisplayControl;

                _context.Update(nanny);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(NannyDetails), new { id = viewModel.NannyResume.Id });
            }

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> EditBaby(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var baby = await _context.BabyResumes.FindAsync(id);
            if (baby == null)
            {
                return NotFound();
            }

            var userInformation = await _context.UserInformations
                                                .FirstOrDefaultAsync(ui => ui.AccountUser == baby.AccountUserAccount);

            var viewModel = new NannyplatformViewModel
            {
                BabyResume = baby,
                UserInformation = userInformation ?? new UserInformation()
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditBaby(NannyplatformViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var baby = await _context.BabyResumes.FindAsync(viewModel.BabyResume.Id);
                if (baby == null)
                {
                    return NotFound();
                }

                baby.AccountUserAccount = viewModel.BabyResume.AccountUserAccount;
                baby.Photo = viewModel.BabyResume.Photo;
                baby.FirstName = viewModel.BabyResume.FirstName;
                baby.City = viewModel.BabyResume.City;
                baby.District = viewModel.BabyResume.District;
                baby.ApplyDate = viewModel.BabyResume.ApplyDate;
                baby.RequireDate = viewModel.BabyResume.RequireDate;
                baby.Babyage = viewModel.BabyResume.Babyage;
                baby.TypeOfDaycare = viewModel.BabyResume.TypeOfDaycare;
                baby.TimeSlot = viewModel.BabyResume.TimeSlot;
                baby.Memo = viewModel.BabyResume.Memo;
                baby.Display = viewModel.BabyResume.Display;

                _context.Update(baby);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(BabyDetails), new { id = viewModel.BabyResume.Id });
            }

            return View(viewModel);
        }
    }
}
