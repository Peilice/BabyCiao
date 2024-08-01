using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BabyCiao_Client.Controllers
{
    //[Area("OnlineCompetitions")]
    public class CompetitionsController : Controller
    {
        // GET: CompetitionsController
        public ActionResult Index()
        {
            return View();
        }

        // GET: CompetitionsController/Details/5
        public ActionResult voteInfo(int id)
        {
            return View();
        }

        // GET: CompetitionsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CompetitionsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CompetitionsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CompetitionsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CompetitionsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

    }
}
