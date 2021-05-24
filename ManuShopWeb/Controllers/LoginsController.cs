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
    public class LoginsController : Controller
    {
        IRepository<Persona, int> repository;
        public LoginsController(IRepository<Persona, int> repository)
        {
            this.repository = repository;
        }

        public IActionResult login()
        {
            return View();
        }

        public IActionResult register()
        {
            return View();
        }


        [HttpPost]
        public IActionResult login(String email, String password)
        {
            ViewData["EMAIL"] = email;
            ViewData["PASSWORD"] = password;
            List<Persona> personasConEmail = this.repository.Get().Where(x => x.email == email).ToList();
            if (personasConEmail.Count() == 0)
            {
                ViewData["MENSAJE"] = "No se ha encontrado su correo en la base de datos";
                ViewData["TITULO"] = "EMAIL NO ENCONTRADO";
                ViewData["MOSTRARMENSAJE"] = "true";
                ViewData["TIPO"] = "WARNING";
                return View();
            }
            else if (personasConEmail.Count() != 1)
            {
                ViewData["MENSAJE"] = "Se ha encontrado su correo repetido en la base de datos";
                ViewData["TITULO"] = "EMAIL DUPLICADO";
                ViewData["MOSTRARMENSAJE"] = "true";
                ViewData["TIPO"] = "DANGER";
                return View();
            }
            else if (this.repository.Get().Where(x => x.email == email && x.contrasena == password).Count() == 0)
            {
                ViewData["MENSAJE"] = "Su contraseña es erronea, vuelva a intentarlo";
                ViewData["TITULO"] = "CONTRASEÑA INCORRECTA";
                ViewData["MOSTRARMENSAJE"] = "true";
                ViewData["TIPO"] = "DANGER";
                return View();
            } 
            else 
            {
                Persona persona = this.repository.Get().Where(x => x.email == email && x.contrasena == password).First();
                if (persona != null)
                {
                    if (persona.tiposPersona == 1)
                    {

                        return RedirectToAction("MisFranquicias", "Franquicias", new { id = persona.id });
                    }
                    else if (persona.tiposPersona == 2)
                    {
                        return RedirectToAction("AllFranquicias", "Franquicias");
                    }
                    else
                    {
                        return View();
                    }
                }
                else
                {
                    ViewData["MENSAJE"] = "La contraseña no coincide";
                    ViewData["TITULO"] = "Contraseña incorrecta";
                    ViewData["MOSTRARMENSAJE"] = "true";
                    ViewData["TIPO"] = "WARNING";
                    return View();
                }
            }
        }

        [HttpPost]
        public IActionResult register(Persona persona)
        {
            ViewData["NOMBRE"] = persona.nombre;
            ViewData["APELLIDOS"] = persona.apellidos;
            ViewData["EMAIL"] = persona.email;
            ViewData["TELEFONO"] = persona.telefono;
            ViewData["PASSWORD"] = persona.contrasena;
            ViewData["TIPOPERSONA"] = persona.tiposPersona;
            ViewData["FECHANACIMIENTO"] = persona.fechaNacimiento;
            List<Persona> personasBuscadas = this.repository.Get().Where(x => x.email == persona.email).ToList();
            if (personasBuscadas.Count == 0)
            {
                this.repository.Create(persona);
                if (persona.tiposPersona == 1)
                {
                    return RedirectToAction("MisFranquicias", "Franquicias", new { id = persona.id });
                }
                else if (persona.tiposPersona == 2)
                {
                    return RedirectToAction("AllFranquicias", "Franquicias");
                }
                else
                {
                    return View();
                }
            }
            else
            {
                ViewData["MENSAJE"] = "Este correo ya esta asociado a una cuenta";
                ViewData["TITULO"] = "Cuenta existente";
                ViewData["MOSTRARMENSAJE"] = "true";
                ViewData["TIPO"] = "WARNING";
                return View();
            }
        }

        public IActionResult logOut(String email)
        {
            ViewData["email"] = email;
            return View();
        }
    }
}
