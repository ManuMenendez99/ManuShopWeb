using ManuShopWeb.Data;
using ManuShopWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManuShopWeb.Repositories
{
    public class TiendaRepository : IRepository<Tienda, int>
    {
        Context context;

        public TiendaRepository(Context context)
        {
            this.context = context;
        }

        public void Create(Tienda model)
        {
            this.context.Tiendas.Add(model);
            this.context.SaveChanges();
        }

        public void Delete(Tienda model)
        {
            this.context.Tiendas.Remove(model);
            this.context.SaveChanges();
        }

        public List<Tienda> Get()
        {
            return this.context.Tiendas.ToList();
        }

        public Tienda GetOne(int id)
        {
            return this.context.Tiendas.Where(x => x.id == id).First();
        }

        public List<Tienda> GetOnes(int id)
        {
            return this.context.Tiendas.Where(x => x.franquicia == id).ToList();
        }

        public void Update(Tienda model)
        {
            this.context.Tiendas.Update(model);
            this.context.SaveChanges();
        }
    }
}
