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
    public class MarcasController : Controller
    {
        IRepository<Marca, int> marcasRepository;
        public MarcasController(IRepository<Marca, int> marcasRepository)
        {
            this.marcasRepository = marcasRepository;
        }

        public ActionResult Index(String email)
        {
            ViewData["EMAIL"] = email;
            return View(this.marcasRepository.Get());
        }

        public ActionResult Create(String email)
        {
            ViewData["EMAIL"] = email;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(String nombre, String email)
        {
            Marca marca = new Marca();
            marca.nombre = nombre;
            this.marcasRepository.Create(marca);

            return RedirectToAction("Index", new { email = email });
        }

        public ActionResult Edit(int id, String email)
        {
            ViewData["EMAIL"] = email;
            return View(this.marcasRepository.GetOne(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(String nombre, String email, int id)
        {
            Marca marca = this.marcasRepository.GetOne(id);
            marca.nombre = nombre;
            this.marcasRepository.Update(marca);
            return RedirectToAction("Index", new { email = email });
        }
    }
}
