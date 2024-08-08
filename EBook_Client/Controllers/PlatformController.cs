using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BabyCiao_Client.Controllers
{
    public class PlatformController : Controller
    {
        // GET: PlatformController
        public ActionResult Index()
        {
            return View();
        }

        // GET: Platform/Details/5
        public ActionResult Article()
        {
            return View();
        }

        // GET: Platform/ArticleInfo
        public ActionResult ArticleInfo()
        {
            return View();
        }


        // GET: Platform/MyArticle
        public ActionResult MyArticle()
        {
            return View();
        }

        // POST: PlatformController/Edit/5
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

        // GET: PlatformController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PlatformController/Delete/5
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
