using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ManuShopWeb.Controllers
{
    public class VersionesProductosController : Controller
    {
        // GET: VersionesProductosController
        public ActionResult Index()
        {
            return View();
        }

        // GET: VersionesProductosController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: VersionesProductosController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VersionesProductosController/Create
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

        // GET: VersionesProductosController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: VersionesProductosController/Edit/5
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

        // GET: VersionesProductosController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: VersionesProductosController/Delete/5
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
