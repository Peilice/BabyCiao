using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BabyCiao_Client.Controllers
{

    public class CompetitionsController : Controller
    {
        // GET: Competitions/Index
        public ActionResult Index()
        {
            return View();
        }

        // GET: Competitions/voteInfo
        public ActionResult voteInfo(int id)
        {
            return View();
        }

        // GET: Competitions/MyCompetition
        public ActionResult MyCompetition()
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
