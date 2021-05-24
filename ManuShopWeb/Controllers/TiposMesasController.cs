using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ManuShopWeb.Controllers
{
    public class TiposMesasController : Controller
    {
        // GET: TiposMesasController
        public ActionResult Index()
        {
            return View();
        }

        // GET: TiposMesasController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TiposMesasController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TiposMesasController/Create
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

        // GET: TiposMesasController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TiposMesasController/Edit/5
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

        // GET: TiposMesasController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TiposMesasController/Delete/5
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
