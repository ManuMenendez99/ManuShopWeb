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
    public class TiendasController : Controller
    {
        IRepository<Tienda, int> repositoryTienda;
        public TiendasController(IRepository<Tienda, int> repositoryTienda)
        {
            this.repositoryTienda = repositoryTienda;
        }
        // GET: TiendasController
        public ActionResult MisTiendas(String origen, int franquicia)
        {
            ViewData["ORIGEN"] = origen;
            List<Tienda> tiendas = new List<Tienda>();
            if (this.repositoryTienda.GetOnes(franquicia).Count() != 0)
            {
                tiendas = this.repositoryTienda.GetOnes(franquicia);
            }
            ViewData["FRANQUICIA"] = franquicia;
            return View(tiendas);
        }

        // GET: TiendasController/Details/5
        public ActionResult Details(int id, String origen)
        {
            Tienda tienda = this.repositoryTienda.GetOne(id);
            ViewData["ORIGEN"] = origen;
            if (origen != "CLIENTE")
            {
                ViewData["FRANQUICIA"] = tienda.franquicia;
            }
            return View(tienda);
        }

        // GET: TiendasController/Create
        public ActionResult Create(int franquicia)
        {
            ViewData["FRANQUICIA"] = franquicia;
            return View();
        }

        // POST: TiendasController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Tienda tienda)
        {
            try
            {
                this.repositoryTienda.Create(tienda);
                return RedirectToAction("MisTiendas", new { franquicia = tienda.franquicia, origen = "TRABAJADOR" });
            }
            catch
            {
                return View();
            }
        }

        // GET: TiendasController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(this.repositoryTienda.GetOne(id));
        }

        // POST: TiendasController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Tienda tienda)
        {
            try
            {
                this.repositoryTienda.Update(tienda);
                return RedirectToAction("MisTiendas", new { franquicia = tienda.franquicia, origen = "TRABAJADOR" });
            }
            catch
            {
                return View();
            }
        }

        // GET: TiendasController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(this.repositoryTienda.GetOne(id));
        }

        // POST: TiendasController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Tienda tiendaOrigen)
        {
            Tienda tienda = this.repositoryTienda.GetOne(tiendaOrigen.id);
            this.repositoryTienda.Delete(tienda);
            return RedirectToAction("MisTiendas", new { franquicia = tienda.franquicia, origen = "TRABAJADOR" });
        }
    }
}
