using Microsoft.EntityFrameworkCore;
using ManuShopWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManuShopWeb.Data
{
    public class Context: DbContext
    {
        public Context(DbContextOptions<Context> options): base(options) { }

        public DbSet<Almacen> Almacenes { get; set; }
        public DbSet<Franquicia> Franquicias { get; set; }
        public DbSet<LineasTicket> LineasTickets { get; set; }
        public DbSet<Marca> Marcas { get; set; }
        public DbSet<Mesa> Mesas { get; set; }
        public DbSet<Persona> Personas { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Tienda> Tiendas { get; set; }
        public DbSet<TiposMesa> TiposMesas { get; set; }
        public DbSet<VersionesProducto> VersionesProductos { get; set; }
    }
}
