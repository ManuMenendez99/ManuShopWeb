using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ManuShopWeb.Data;
using ManuShopWeb.Models;
using ManuShopWeb.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ManuShopWeb
{
    public class Startup
    {
        IConfiguration Configuration;

        public Startup(IConfiguration Configuration)
        {
            this.Configuration = Configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            String Sqlserver = this.Configuration.GetConnectionString("SqlServer");
            String mysql = this.Configuration.GetConnectionString("MySql");

            services.AddDbContextPool<Context>(options => options.UseMySql(mysql, ServerVersion.AutoDetect(mysql)));
            //services.AddDbContext<Context>(options => options.UseSqlServer(Sqlserver));
            services.AddTransient<IRepository<Almacen, int>, AlmacenesRepository>();
            services.AddTransient<IRepository<Franquicia, int>, FranquiciasRepository>();
            services.AddTransient<IRepository<LineasTicket, int>, LineasTicketRepository>();
            services.AddTransient<IRepository<Marca, int>, MarcaRepository>();
            services.AddTransient<IRepository<Mesa, int>, MesaRepository>();
            services.AddTransient<IRepository<Persona, int>, PersonaRepository>();
            services.AddTransient<IRepository<Producto, int>, ProductoRepository>();
            services.AddTransient<IRepository<Ticket, int>, TicketRepository>();
            services.AddTransient<IRepository<Tienda, int>, TiendaRepository>();
            services.AddTransient<IRepository<TiposMesa, int>, TiposMesaRepository>();
            services.AddTransient<IRepository<VersionesProducto, int>, VersionesProductoRepository>();


            //services.AddTransient<IRepository<Coche, int>, RepositoryMySql>();

            //services.AddTransient<IRepository<Coche, int>, RepositoryXML>();
            //services.AddTransient<PathProvider>();
            //services.AddTransient<UploadService>();


            services.AddControllersWithViews();

        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseStaticFiles();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Logins}/{action=login}/{id?}"
                    );

            });
        }
    }
}
