using ManuShopWeb.Data;
using ManuShopWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManuShopWeb.Repositories
{
    public class ProductoRepository: IRepository<Producto, int>
    {
        Context context;

        public ProductoRepository(Context context)
        {
            this.context = context;
        }
        public void Create(Producto model)
        {
            this.context.Productos.Add(model);
            this.context.SaveChanges();
        }

        public void Delete(Producto model)
        {
            this.context.Productos.Remove(model);
            this.context.SaveChanges();
        }

        public List<Producto> Get()
        {
            return this.context.Productos.ToList();
        }

        public Producto GetOne(int id)
        {
            return this.context.Productos.Where(x => x.id == id).First();
        }

        public List<Producto> GetOnes(int id)
        {
            return this.context.Productos.Where(x => x.marca == id).ToList();
        }

        public void Update(Producto model)
        {
            this.context.Productos.Update(model);
            this.context.SaveChanges();
        }
    }
}
