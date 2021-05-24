using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ManuShopWeb.Data;
using ManuShopWeb.Models;
using ManuShopWeb.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ManuShopWeb.Controllers
{
    public class ProductosController : Controller
    {
        IRepository<Producto, int> productosRepository;
        IRepository<Marca, int> marcasRepository;
        IRepository<Almacen, int> almacenRepository;
        IRepository<VersionesProducto, int> versionesRepository;
        IRepository<Tienda, int> tiendasRepository;
        IRepository<Franquicia, int> franquiciasRepository;
        IRepository<Persona, int> personasRepository;
        Context context;

        public ProductosController(Context context, IRepository<Producto, int> productosRepository, IRepository<Marca, int> marcasRepository, IRepository<Almacen, int> almacenRepository, IRepository<VersionesProducto, int> versionesRepository, IRepository<Tienda, int> tiendasRepository, IRepository<Franquicia, int> franquiciasRepository, IRepository<Persona, int> personasRepository)
        {
            this.productosRepository = productosRepository;
            this.marcasRepository = marcasRepository;
            this.almacenRepository = almacenRepository;
            this.versionesRepository = versionesRepository;
            this.tiendasRepository = tiendasRepository;
            this.franquiciasRepository = franquiciasRepository;
            this.personasRepository = personasRepository;
            this.context = context;
        }
        // GET: ProductosController
        public ActionResult Index(String origen, String email)
        {
            List<Tienda> tiendas = this.tiendasRepository.Get();
            List<Producto> productos = this.productosRepository.Get();
            List<Marca> marcas = this.marcasRepository.Get();
            List<Franquicia> franquicias = this.franquiciasRepository.Get();
            List<VersionesProducto> versionesProducto = this.versionesRepository.Get();
            ViewData["ALMACEN"] = new List<Almacen>();
            ViewData["MARCAS"] = marcas;
            ViewData["MISFRANQUICIAS"] = new List<Franquicia>();
            ViewData["MISTIENDAS"] = new List<Tienda>();
            ViewData["MIALMACEN"] = new List<Almacen>();
            ViewData["VERSIONES"] = versionesProducto;
            ViewData["ORIGEN"] = origen;
            ViewData["EMAIL"] = email;
            if (origen == "TRABAJADOR")
            {
                Persona persona = this.personasRepository.Get().Where(x => x.email == email).FirstOrDefault();
                List<Franquicia> misFranquicias = this.franquiciasRepository.GetOnes(persona.id);
                List<Tienda> misTiendas = this.tiendasRepository.Get().Where(x => misFranquicias.Select(y => y.id).ToList().Contains(x.franquicia)).ToList();
                List<Almacen> almacen = this.almacenRepository.Get().Where(x => misTiendas.Select(y => y.id).Contains(x.tienda)).ToList();
                ViewData["MIALMACEN"] = almacen;
                ViewData["MISFRANQUICIAS"] = misFranquicias;
                ViewData["MISTIENDAS"] = misTiendas;
            }


            return View(productos);
        }

        // GET: ProductosController/Create
        public ActionResult Create(String email)
        {
            ViewData["MARCAS"] = this.marcasRepository.Get();
            ViewData["EMAIL"] = email;
            return View();
        }

        // POST: ProductosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(String nombre, int marca, int precio, String email)
        {
            Producto producto = new Producto();
            producto.marca = marca;
            producto.nombre = nombre;
            this.productosRepository.Create(producto);
            int idNuevo = this.productosRepository.Get().First(x => x.marca == marca && x.nombre == nombre).id;
            VersionesProducto versionProducto = new VersionesProducto();
            versionProducto.nombre = "default";
            versionProducto.precio = precio;
            versionProducto.producto = idNuevo;
            this.versionesRepository.Create(versionProducto);
            return RedirectToAction("Index", new { email = email, origen = "TRABAJADOR" });
        }

        // GET: ProductosController/Edit/5
        public ActionResult Edit(int version, String email)
        {
            VersionesProducto versionEncontrada = this.versionesRepository.GetOne(version);
            Producto producto = this.productosRepository.GetOne(versionEncontrada.producto);
            List<VersionesProducto> versionesProducto = this.versionesRepository.GetOnes(producto.id);
            ViewData["VERSIONES"] = versionesProducto;
            ViewData["MARCAS"] = this.marcasRepository.Get();
            ViewData["EMAIL"] = email;
            return View(producto);
        }

        // POST: ProductosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int marca, String productoNombre, int id, String email)
        {
            Producto producto = this.productosRepository.GetOne(id);
            producto.nombre = productoNombre;
            producto.marca = marca;

            this.productosRepository.Update(producto);
            return RedirectToAction("Index", new { email = email, origen = "TRABAJADOR" });



        }

        // GET: ProductosController/Delete/5
        public ActionResult EditarVersionProducto(String email, int version)
        {
            ViewData["EMAIL"] = email;
            return View(this.versionesRepository.GetOne(version));
        }

        [HttpPost]
        public ActionResult EditarVersionProducto(String email, String nombre, String detalles, int precio, int id)
        {
            VersionesProducto version = this.versionesRepository.GetOne(id);
            version.nombre = nombre;
            version.precio = precio;
            version.detalles = detalles;
            this.versionesRepository.Update(version);
            return RedirectToAction("Index", new { email = email, origen = "TRABAJADOR" });
        }

        public ActionResult Stock(int id, String email, int tienda, int cantidad)
        {
            Persona persona = this.personasRepository.Get().Where(x => x.email == email).FirstOrDefault();
            Tienda tiendaEncontrada = this.tiendasRepository.GetOne(tienda);
            VersionesProducto version = this.versionesRepository.GetOne(id);
            Producto producto = this.productosRepository.GetOne(version.producto);
            Marca marca = this.marcasRepository.GetOne(producto.marca);

            ViewData["TIENDA"] = tiendaEncontrada;
            ViewData["VERSION"] = version;
            ViewData["PRODUCTO"] = producto;
            ViewData["CANTIDAD"] = cantidad;
            ViewData["EMAIL"] = email;
            ViewData["MARCA"] = marca;

            return View();
        }
        [HttpPost]
        public IActionResult Stock(int producto, int cantidad, int tienda)
        {
            List<Almacen> lineasAlmacen = this.almacenRepository.Get().Where(x => x.tienda == tienda && x.versionProducto == producto).ToList();
            if (lineasAlmacen.Count() == 0)
            {
                Almacen linea = new Almacen();
                linea.cantidad = cantidad;
                linea.versionProducto = producto;
                linea.tienda = tienda;
                this.almacenRepository.Create(linea);
            }
            else
            {
                lineasAlmacen.First().cantidad = cantidad;
                this.almacenRepository.Update(lineasAlmacen.First());
            }
            return RedirectToAction("Index", new { email = this.personasRepository.GetOne(this.franquiciasRepository.GetOne(this.tiendasRepository.GetOne(tienda).franquicia).jefe).email, origen = "TRABAJADOR" });
        }
        public IActionResult StockDesdeCero(String email)
        {
            Persona persona = this.personasRepository.Get().Where(x => x.email == email).FirstOrDefault();
            List<Franquicia> franquicias = this.franquiciasRepository.GetOnes(persona.id);
            List<Tienda> tiendas = this.tiendasRepository.Get().Where(x => franquicias.Select(y => y.id).Contains(x.franquicia)).ToList();
            List<VersionesProducto> versiones = this.versionesRepository.Get();
            List<Producto> productos = this.productosRepository.Get();
            List<Marca> marcas = this.marcasRepository.Get();

            ViewData["TIENDAS"] = tiendas;
            ViewData["FRANQUICIAS"] = franquicias;
            ViewData["VERSIONES"] = versiones;
            ViewData["PRODUCTOS"] = productos;
            ViewData["EMAIL"] = email;
            ViewData["MARCAS"] = marcas;
            return View();
        }

        [HttpPost]
        public IActionResult StockDesdeCero(int producto, int cantidad, int tienda)
        {
            List<Almacen> lineasAlmacen = this.almacenRepository.Get().Where(x => x.tienda == tienda && x.versionProducto == producto).ToList();
            if (lineasAlmacen.Count() == 0)
            {
                Almacen linea = new Almacen();
                linea.cantidad = cantidad;
                linea.versionProducto = producto;
                linea.tienda = tienda;
                this.almacenRepository.Create(linea);
            }
            else
            {
                Almacen lineaAlmacen = lineasAlmacen.First();
                lineaAlmacen.cantidad = cantidad;
                this.almacenRepository.Update(lineaAlmacen);
            }
            return RedirectToAction("Index", new { email = this.personasRepository.GetOne(this.franquiciasRepository.GetOne(this.tiendasRepository.GetOne(tienda).franquicia).jefe).email, origen = "TRABAJADOR" });
        }
    }

}
