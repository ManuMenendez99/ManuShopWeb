using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ManuShopWeb.Models;
using ManuShopWeb.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ManuShopWeb.Controllers
{
    public class LayoutController : Controller
    {
        IRepository<Persona, int> personasRepository;
        public LayoutController(IRepository<Persona, int> personasRepository)
        {
            this.personasRepository = personasRepository;
        }
        public IActionResult Franquicias(String email)
        {
            Persona persona = this.personasRepository.Get().Where(x => x.email == email).First();
            if (persona.tiposPersona == 1)
            {
                return RedirectToAction("MisFranquicias", "Franquicias", new { id = persona.id });
            }
            else
            {
                return RedirectToAction("AllFranquicias", "Franquicias");
            }
        }

        public IActionResult Productos(String email)
        {
            Persona persona = this.personasRepository.Get().Where(x => x.email == email).First();
            return RedirectToAction("Index", "Productos", new { origen = persona.tiposPersona == 1 ? "TRABAJADOR" : "CLIENTE", email = email });
        }

        public IActionResult Logout(String email)
        {
            return RedirectToAction("LogOut", "Logins", new { email = email });
        }
    }
}
