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
    public class TicketsController : Controller
    {

        IRepository<Ticket, int> ticketsRepository;
        IRepository<Mesa, int> mesasRepository;
        IRepository<TiposMesa, int> tiposMesaRepository;
        IRepository<LineasTicket, int> lineasTicketRepository;
        IRepository<Producto, int> productosRepository;
        IRepository<VersionesProducto, int> versionesProductosRepository;
        IRepository<Almacen, int> almacenRepository;
        IRepository<Marca, int> marcasRepository;

        public TicketsController(IRepository<Marca, int> marcasRepository, IRepository<Almacen, int> almacenRepository, IRepository<Producto, int> productosRepository, IRepository<Ticket, int> ticketsRepository, IRepository<Mesa, int> mesasRepository, IRepository<TiposMesa, int> tiposMesaRepository, IRepository<LineasTicket, int> lineasTicketRepository, IRepository<VersionesProducto, int> versionesProductosRepository)
        {
            this.ticketsRepository = ticketsRepository;
            this.mesasRepository = mesasRepository;
            this.tiposMesaRepository = tiposMesaRepository;
            this.lineasTicketRepository = lineasTicketRepository;
            this.productosRepository = productosRepository;
            this.versionesProductosRepository = versionesProductosRepository;
            this.almacenRepository = almacenRepository;
            this.marcasRepository = marcasRepository;
        }

        public ActionResult VerTicket(int ticket, String origen, String error)
        {
            Ticket ticketEncontrado = this.ticketsRepository.GetOne(ticket);
            Mesa mesaEncontrada = this.mesasRepository.GetOne(ticketEncontrado.mesa);
            TiposMesa tipoMesa = this.tiposMesaRepository.GetOne(mesaEncontrada.tipoMesa);
            List<LineasTicket> lineasTicket = this.lineasTicketRepository.GetOnes(ticketEncontrado.id);
            List<Producto> productos = this.productosRepository.Get();
            List<VersionesProducto> versionesProductos = this.versionesProductosRepository.Get();
            List<Almacen> almacen = this.almacenRepository.GetOnes(ticketEncontrado.tienda);
            double precioTotal = 0;

            lineasTicket.Where(x => x.cancelada == false).ToList().ForEach(linea =>
            {
                int cantidad = linea.cantidad;
                double precio = versionesProductos.Where(version => version.id == linea.versionProducto).First().precio;
                int descuento = linea.descuento;

                precioTotal += ((precio * cantidad * (100 - descuento)) / 100);
            });

            precioTotal = precioTotal - ticketEncontrado.descuentoGeneral;

            ViewData["ALMACEN"] = almacen;
            ViewData["MESA"] = mesaEncontrada;
            ViewData["TIPOMESA"] = tipoMesa;
            ViewData["TICKET"] = ticketEncontrado;
            ViewData["PRODUCTOS"] = productos;
            ViewData["VERSIONESPRODUCTOS"] = versionesProductos;
            ViewData["TOTAL"] = Math.Round(double.Parse(precioTotal.ToString() + "0"), 3);
            ViewData["ORIGEN"] = origen;
            ViewData["ERROR"] = error;
            return View(lineasTicket);
        }

        [HttpPost]
        public ActionResult VerTicket(int ticket, int descuento)
        {
            Ticket ticketEncontrado = this.ticketsRepository.GetOne(ticket);
            String origen = "TRABAJADOR";
            ticketEncontrado.descuentoGeneral = descuento;
            this.ticketsRepository.Update(ticketEncontrado);
            ticketEncontrado = this.ticketsRepository.GetOne(ticket);
            Mesa mesaEncontrada = this.mesasRepository.GetOne(ticketEncontrado.mesa);
            TiposMesa tipoMesa = this.tiposMesaRepository.GetOne(mesaEncontrada.tipoMesa);
            List<LineasTicket> lineasTicket = this.lineasTicketRepository.GetOnes(ticketEncontrado.id);
            List<Producto> productos = this.productosRepository.Get();
            List<VersionesProducto> versionesProductos = this.versionesProductosRepository.Get();
            List<Almacen> almacen = this.almacenRepository.GetOnes(ticketEncontrado.tienda);
            double precioTotal = 0;

            lineasTicket.Where(x => x.cancelada == false).ToList().ForEach(linea =>
            {
                int cantidad = linea.cantidad;
                double precio = versionesProductos.Where(version => version.id == linea.versionProducto).First().precio;
                int descuento = linea.descuento;

                precioTotal += ((precio * cantidad * (100 - descuento)) / 100);
            });

            precioTotal = precioTotal - ticketEncontrado.descuentoGeneral;

            ViewData["ALMACEN"] = almacen;
            ViewData["MESA"] = mesaEncontrada;
            ViewData["TIPOMESA"] = tipoMesa;
            ViewData["TICKET"] = ticketEncontrado;
            ViewData["PRODUCTOS"] = productos;
            ViewData["VERSIONESPRODUCTOS"] = versionesProductos;
            ViewData["TOTAL"] = Math.Round(double.Parse(precioTotal.ToString() + "0"), 3);
            ViewData["ORIGEN"] = origen;
            ViewData["ERROR"] = "";
            return View(lineasTicket);
        }

        public ActionResult EditLinea(int id)
        {
            Ticket ticketEncontrado = this.ticketsRepository.GetOne(id);
            List<Producto> productos = this.productosRepository.Get();
            List<VersionesProducto> versionesProductos = this.versionesProductosRepository.Get();
            List<Almacen> almacen = this.almacenRepository.GetOnes(ticketEncontrado.tienda);
            List<Marca> marcas = this.marcasRepository.Get();
            
            ViewData["ALMACEN"] = almacen;
            ViewData["VERSIONESPRODUCTOS"] = versionesProductos;
            ViewData["PRODUCTOS"] = productos;
            ViewData["MARCAS"] = marcas;

            return View(this.lineasTicketRepository.GetOne(id));
        }

        public ActionResult SumarUnidad(int id)
        {
            LineasTicket linea = this.lineasTicketRepository.GetOne(id);
            linea.cantidad += 1;
            this.lineasTicketRepository.Update(linea);
            return RedirectToAction("", new { ticket = linea.ticket, origen = "CLIENTE" });
        }

        [HttpPost]
        public ActionResult EditLinea(LineasTicket linea)
        {
            LineasTicket lineaAntigua = this.lineasTicketRepository.GetOne(linea.id);

            int versionAntigua = lineaAntigua.versionProducto;
            int cantidadAntigua = lineaAntigua.cantidad;
            List<Almacen> almacenActual = this.almacenRepository.Get().Where(x => x.tienda == this.ticketsRepository.GetOne(linea.ticket).tienda).ToList();

            Almacen LineaAlmacenAntigua = almacenActual.Where(x => x.versionProducto == versionAntigua).FirstOrDefault();

            LineaAlmacenAntigua.cantidad += cantidadAntigua;

            Almacen lineaAlmacenNueva = almacenActual.Where(x => x.versionProducto == linea.versionProducto).FirstOrDefault();

            lineaAlmacenNueva.cantidad = lineaAlmacenNueva.cantidad - linea.cantidad;


            this.almacenRepository.Update(LineaAlmacenAntigua);
            this.almacenRepository.Update(lineaAlmacenNueva);
            this.lineasTicketRepository.Update(linea);
            return RedirectToAction("VerTicket", new { ticket = linea.ticket, origen = "TRABAJADOR" });
        }

        public ActionResult DeleteLinea(int id)
        {
            LineasTicket lineaEncontrada = this.lineasTicketRepository.GetOne(id);
            lineaEncontrada.cancelada = true;
            Almacen LineaAlmacenAntigua = this.almacenRepository.Get().Where(x => x.tienda == this.ticketsRepository.GetOne(lineaEncontrada.ticket).tienda).FirstOrDefault();
            LineaAlmacenAntigua.cantidad += lineaEncontrada.cantidad;
            this.almacenRepository.Update(LineaAlmacenAntigua);
            this.lineasTicketRepository.Update(lineaEncontrada);
            return RedirectToAction("VerTicket", new { ticket = lineaEncontrada.ticket, origen = "TRABAJADOR" });
        }

        public ActionResult ActivarLinea(int id)
        {
            LineasTicket lineaEncontrada = this.lineasTicketRepository.GetOne(id);
            lineaEncontrada.cancelada = false;
            Almacen LineaAlmacenAntigua = this.almacenRepository.Get().Where(x => x.tienda == this.ticketsRepository.GetOne(lineaEncontrada.ticket).tienda).FirstOrDefault();
            LineaAlmacenAntigua.cantidad = LineaAlmacenAntigua.cantidad - lineaEncontrada.cantidad;
            String error = null;
            if (LineaAlmacenAntigua.cantidad >= 0)
            {
            this.almacenRepository.Update(LineaAlmacenAntigua);
            this.lineasTicketRepository.Update(lineaEncontrada);
            } else
            {
                error = "No se puede hacer no hay suficiente stock";
            }
            return RedirectToAction("VerTicket", new { ticket = lineaEncontrada.ticket, origen = "TRABAJADOR", error = error });
        }

        public ActionResult InsertarProducto(int ticket, String origen)
        {
            Ticket ticketEncontrado = this.ticketsRepository.GetOne(ticket);
            List<Almacen> almacen = this.almacenRepository.GetOnes(ticketEncontrado.tienda);
            List<int> VersionesProductosAlmacen = almacen.Select(y => y.versionProducto).ToList();
            List<VersionesProducto> versionesProducto = this.versionesProductosRepository.Get().Where(x => VersionesProductosAlmacen.Contains(x.id)).ToList();
            List<int> IntegersProductos = versionesProducto.Select(x => x.producto).Distinct().ToList();
            List<Producto> Productos = this.productosRepository.Get().Where(x => IntegersProductos.Contains(x.id)).ToList();
            List<Marca> marcas = this.marcasRepository.Get().Where(x => Productos.Select(y => y.marca).Contains(x.id)).ToList();

            ViewData["TICKET"] = ticketEncontrado;
            ViewData["ALMACEN"] = almacen;
            ViewData["VERSIONESPRODUCTOS"] = versionesProducto;
            ViewData["PRODUCTOS"] = Productos;
            ViewData["ORIGEN"] = origen;
            ViewData["MARCAS"] = marcas;
            ViewData["ERROR"] = null;

            return View();
        }

        [HttpPost]
        public ActionResult InsertarProducto(String origen, int ticket, String detalles, int cantidad, int producto, int descuento)
        {
            if (this.almacenRepository.Get().Where(x => x.versionProducto == producto && x.tienda == this.ticketsRepository.GetOne(ticket).tienda).FirstOrDefault().cantidad >= cantidad)
            {
                //Hay que buscar si existe ya una linea en el ticket con este producto
                List<LineasTicket> lineasTicket = this.lineasTicketRepository.GetOnes(ticket).Where(x => x.versionProducto == producto).ToList();
                if (lineasTicket.Count != 0)
                {
                    LineasTicket lineaTicket = lineasTicket.First();
                    lineaTicket.cantidad += cantidad;
                    lineaTicket.detalles += " - " + detalles;
                    this.lineasTicketRepository.Update(lineaTicket);
                }
                else
                {
                    // Hay que poner una linea en el ticket
                    LineasTicket lineaticket = new LineasTicket();
                    lineaticket.descuento = descuento;
                    lineaticket.cantidad = cantidad;
                    lineaticket.detalles = detalles;
                    lineaticket.ticket = ticket;
                    lineaticket.cancelada = false;
                    lineaticket.versionProducto = producto;
                    this.lineasTicketRepository.Create(lineaticket);
                    // Hay que quitar los productos
                    Almacen almacen = this.almacenRepository.Get().Where(x => x.versionProducto == producto && x.tienda == this.ticketsRepository.GetOne(ticket).tienda).FirstOrDefault();
                    almacen.cantidad = almacen.cantidad - cantidad;
                    this.almacenRepository.Update(almacen);
                }
                return RedirectToAction("VerTicket", new { ticket = ticket, origen = origen });
            }
            else
            {
                Ticket ticketEncontrado = this.ticketsRepository.GetOne(ticket);
                List<Almacen> almacen = this.almacenRepository.GetOnes(ticketEncontrado.tienda);
                List<int> VersionesProductosAlmacen = almacen.Select(y => y.versionProducto).ToList();
                List<VersionesProducto> versionesProducto = this.versionesProductosRepository.Get().Where(x => VersionesProductosAlmacen.Contains(x.id)).ToList();
                List<int> IntegersProductos = versionesProducto.Select(x => x.producto).Distinct().ToList();
                List<Producto> Productos = this.productosRepository.Get().Where(x => IntegersProductos.Contains(x.id)).ToList();
                List<Marca> marcas = this.marcasRepository.Get().Where(x => Productos.Select(y => y.marca).Contains(x.id)).ToList();

                ViewData["TICKET"] = ticketEncontrado;
                ViewData["ALMACEN"] = almacen;
                ViewData["VERSIONESPRODUCTOS"] = versionesProducto;
                ViewData["PRODUCTOS"] = Productos;
                ViewData["ORIGEN"] = origen;
                ViewData["MARCAS"] = marcas;
                ViewData["ERROR"] = "No hay unidades suficientes en el almacen";

                return View();
            }
        }

        public IActionResult CerrarCuenta(int id)
        {
            Ticket ticket = this.ticketsRepository.GetOne(id);
            Mesa mesa = this.mesasRepository.GetOne(ticket.mesa);
            ticket.fechaCierre = DateTime.Now.ToString();
            mesa.estado = 2;
            this.ticketsRepository.Update(ticket);
            this.mesasRepository.Update(mesa);
            return RedirectToAction("VerTicket", new { ticket = ticket.id, origen = "CLIENTE" });
        }

        public IActionResult CobrarTicket(int id)
        {
            Ticket ticket = this.ticketsRepository.GetOne(id);
            Mesa mesa = this.mesasRepository.GetOne(ticket.mesa);
            mesa.estado = 0;
            this.mesasRepository.Update(mesa);
            return RedirectToAction("ResumenMesas", "Mesas", new { tienda = this.mesasRepository.GetOne(ticket.mesa).tienda, origen = "TRABAJADOR" });
        }
    }
}
