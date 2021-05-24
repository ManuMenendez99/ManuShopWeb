using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ManuShopWeb.Models;
using ManuShopWeb.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ManuShopWeb.Controllers
{
    public class FranquiciasController : Controller
    {
        IRepository<Franquicia, int> repositoryFranquicias;
        public FranquiciasController(IRepository<Franquicia, int> repository)
        {
            this.repositoryFranquicias = repository;
        }
        // GET: FranquiciasController
        public ActionResult MisFranquicias(int id)
        {
            return View(this.repositoryFranquicias.GetOnes(id));
        }

        public ActionResult AllFranquicias()
        {
            return View(this.repositoryFranquicias.Get());
        }

        // GET: FranquiciasController/Details/5
        public ActionResult Details(int id, String origen)
        {
            Franquicia franquicia = this.repositoryFranquicias.GetOne(id);
            ViewData["ORIGEN"] = origen;
            if (origen != "CLIENTE")
            {
                ViewData["JEFE"] = franquicia.jefe;
            }
            return View(franquicia);
        }

        // GET: FranquiciasController/Create
        public ActionResult Create(int jefe)
        {
            ViewData["JEFE"] = jefe;
            return View();
        }

        // POST: FranquiciasController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Franquicia franquicia)
        {
            try
            {
                this.repositoryFranquicias.Create(franquicia);
                return RedirectToAction("MisFranquicias", new { id = franquicia.jefe });
            }
            catch
            {
                return View();
            }
        }

        // GET: FranquiciasController/Edit/5
        public ActionResult Edit(int id)
        {
            Franquicia franquicia = this.repositoryFranquicias.GetOne(id);
            ViewData["JEFE"] = franquicia.jefe;
            return View(franquicia);
        }

        // POST: FranquiciasController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Franquicia franquicia)
        {
            try
            {
                this.repositoryFranquicias.Update(franquicia);
                return RedirectToAction("MisFranquicias", new { id = franquicia.jefe });
            }
            catch
            {
                return View();
            }
        }

        // GET: FranquiciasController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(this.repositoryFranquicias.GetOne(id));
        }

        // POST: FranquiciasController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Franquicia franquicia, int jefe, String nombre)
        {
            this.repositoryFranquicias.Delete(franquicia);
            return RedirectToAction("MisFranquicias", "Franquicias", new { id = jefe });


        }
    }
}
