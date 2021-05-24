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
    public class MesasController : Controller
    {
        IRepository<Mesa, int> repositoryMesas;
        IRepository<TiposMesa, int> repositoryTiposMesas;
        IRepository<Ticket, int> repositoryTickets;
        public MesasController(IRepository<Mesa, int> repositoryMesas, IRepository<TiposMesa, int> repositoryTiposMesas, IRepository<Ticket, int> repositoryTickets)
        {
            this.repositoryMesas = repositoryMesas;
            this.repositoryTiposMesas = repositoryTiposMesas;
            this.repositoryTickets = repositoryTickets;
        }
        // GET: Mesas
        public ActionResult ResumenMesas(int tienda)
        {
            ViewData["TIPOSMESA"] = this.repositoryTiposMesas.Get();
            ViewData["TICKETS"] = this.repositoryTickets.Get();
            ViewData["TIENDA"] = tienda;
            return View(this.repositoryMesas.GetOnes(tienda));
        }

        public ActionResult BuscarMesa(int tienda)
        {
            ViewData["TIPOSMESA"] = this.repositoryTiposMesas.Get();
            ViewData["TIENDA"] = tienda;
            return View(this.repositoryMesas.GetOnes(tienda));
        }

        [HttpPost]
        public ActionResult BuscarMesa(int mesa, int capacidad, String detalles)
        {
            Mesa mesaEncontrada = this.repositoryMesas.GetOne(mesa);
            mesaEncontrada.estado = 1;
            this.repositoryMesas.Update(mesaEncontrada);

            Ticket ticket = new Ticket();
            ticket.detalles = detalles;
            ticket.fechaApertura = DateTime.Now.ToString();
            ticket.personas = capacidad;
            ticket.mesa = mesa;
            ticket.tienda = mesaEncontrada.tienda;

            this.repositoryTickets.Create(ticket);
            int idTicketNuevo = this.repositoryTickets.Get().Where(x => x.fechaApertura == ticket.fechaApertura).First().id;

            return RedirectToAction("VerTicket", "Tickets", new { ticket = idTicketNuevo, origen = "CLIENTE" });
        }

        // GET: Mesas/Details/5
        public ActionResult Details(int id, String origen)
        {
            ViewData["TIPOSMESA"] = this.repositoryTiposMesas.Get();
            ViewData["ORIGEN"] = origen;
            return View(this.repositoryMesas.GetOne(id));
        }

        // GET: Mesas/Create
        public ActionResult Create(int tienda)
        {
            ViewData["TIPOSMESA"] = this.repositoryTiposMesas.Get();
            ViewData["TIENDA"] = tienda;
            return View();
        }

        // POST: Mesas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Mesa mesa)
        {
            try
            {
                this.repositoryMesas.Create(mesa);
                return RedirectToAction("ResumenMesas", new { tienda = mesa.tienda });
            }
            catch
            {
                return View();
            }
        }

        // GET: Mesas/Edit/5
        public ActionResult Edit(int id)
        {
            ViewData["TIPOSMESA"] = this.repositoryTiposMesas.Get();
            return View(this.repositoryMesas.GetOne(id));
        }

        // POST: Mesas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Mesa mesa)
        {
            this.repositoryMesas.Update(mesa);
            return RedirectToAction("ResumenMesas", new { tienda = mesa.tienda });

        }

        // GET: Mesas/Delete/5
        public ActionResult Delete(int id)
        {
            ViewData["TIPOSMESA"] = this.repositoryTiposMesas.Get();
            return View(this.repositoryMesas.GetOne(id));
        }

        // POST: Mesas/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Mesa mesaSinPulir)
        {
            Mesa mesa = this.repositoryMesas.GetOne(mesaSinPulir.id);
            this.repositoryMesas.Delete(mesa);
            return RedirectToAction("ResumenMesas", new { tienda = mesa.tienda });
        }

        public ActionResult CrearTipoMesa(int tienda)
        {
            ViewData["TIENDA"] = tienda;
            return View();
        }

        [HttpPost]
        public ActionResult CrearTipoMesa(String nombre, String detalles, int tienda)
        {
            TiposMesa tipo = new TiposMesa();
            tipo.nombre = nombre;
            tipo.detalles = detalles;
            this.repositoryTiposMesas.Create(tipo);
            return RedirectToAction("ResumenMesas", new { tienda = tienda });
        }
    }
}
