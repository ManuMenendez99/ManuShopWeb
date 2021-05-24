using ManuShopWeb.Data;
using ManuShopWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManuShopWeb.Repositories
{
    public class MesaRepository : IRepository<Mesa, int>
    {
        Context context;
        public MesaRepository(Context context)
        {
            this.context = context;
        }

        public void Create(Mesa model)
        {
            this.context.Mesas.Add(model);
            this.context.SaveChanges();
        }

        public void Delete(Mesa model)
        {
            this.context.Mesas.Remove(model);
            this.context.SaveChanges();
        }

        public List<Mesa> Get()
        {
            return this.context.Mesas.ToList();
        }

        public Mesa GetOne(int id)
        {
            return this.context.Mesas.Where(x => x.id == id).First();
        }

        public List<Mesa> GetOnes(int id)
        {
            return this.context.Mesas.Where(x => x.tienda == id).ToList();
        }

        public void Update(Mesa model)
        {
            this.context.Mesas.Update(model);
            this.context.SaveChanges();
        }
    }
}
