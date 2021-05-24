using ManuShopWeb.Data;
using ManuShopWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManuShopWeb.Repositories
{
    public class VersionesProductoRepository: IRepository<VersionesProducto, int>
    {
        Context context;

        public VersionesProductoRepository(Context context)
        {
            this.context = context;
        }

        public void Create(VersionesProducto model)
        {
            this.context.VersionesProductos.Add(model);
            this.context.SaveChanges();
        }

        public void Delete(VersionesProducto model)
        {
            this.context.VersionesProductos.Remove(model);
            this.context.SaveChanges();
        }

        public List<VersionesProducto> Get()
        {
            return this.context.VersionesProductos.ToList();
        }

        public VersionesProducto GetOne(int id)
        {
            return this.context.VersionesProductos.Where(x => x.id == id).First();
        }

        public List<VersionesProducto> GetOnes(int id)
        {
            return this.context.VersionesProductos.Where(x => x.producto == id).ToList();
        }

        public void Update(VersionesProducto model)
        {
            this.context.VersionesProductos.Update(model);
            this.context.SaveChanges();
        }
    }
}
