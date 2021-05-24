using ManuShopWeb.Data;
using ManuShopWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManuShopWeb.Repositories
{
    public class TiposMesaRepository: IRepository<TiposMesa, int>
    {
        Context context;

        public TiposMesaRepository(Context context)
        {
            this.context = context;
        }

        public void Create(TiposMesa model)
        {
            this.context.TiposMesas.Add(model);
            this.context.SaveChanges();
        }

        public void Delete(TiposMesa model)
        {
            this.context.TiposMesas.Remove(model);
            this.context.SaveChanges();
        }

        public List<TiposMesa> Get()
        {
            return this.context.TiposMesas.ToList();
        }

        public TiposMesa GetOne(int id)
        {
            return this.context.TiposMesas.Where(x => x.id == id).First();
        }

        public List<TiposMesa> GetOnes(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(TiposMesa model)
        {
            this.context.TiposMesas.Update(model);
            this.context.SaveChanges();
        }
    }
}
