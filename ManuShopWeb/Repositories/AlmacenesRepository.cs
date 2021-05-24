using ManuShopWeb.Data;
using ManuShopWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManuShopWeb.Repositories
{
    public class AlmacenesRepository: IRepository<Almacen, int>
    {
        Context context;
        public AlmacenesRepository(Context context)
        {
            this.context = context;
        }
        public List<Almacen> Get()
        {
            return this.context.Almacenes.ToList();
        }
        public Almacen GetOne(int id)
        {
            return this.context.Almacenes.Where(x => x.id == id).First();
        }
        public List<Almacen> GetOnes(int id)
        {
            return this.context.Almacenes.Where(x => x.tienda == id).ToList();
        }
        public void Create(Almacen almacen)
        {
            this.context.Almacenes.Add(almacen);
            this.context.SaveChanges();
        }
        public void Update(Almacen almacen)
        {
            this.context.Almacenes.Update(almacen);
            this.context.SaveChanges();
        }
        public void Delete(Almacen almacen)
        {
            this.context.Almacenes.Remove(almacen);
            this.context.SaveChanges();
        }
    }
}
