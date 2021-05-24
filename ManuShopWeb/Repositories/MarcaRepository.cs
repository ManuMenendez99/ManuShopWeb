using ManuShopWeb.Data;
using ManuShopWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManuShopWeb.Repositories
{
    public class MarcaRepository : IRepository<Marca, int>
    {
        Context context;

        public MarcaRepository(Context context)
        {
            this.context = context;
        }
        public void Create(Marca model)
        {
            this.context.Marcas.Add(model);
            this.context.SaveChanges();
        }

        public void Delete(Marca model)
        {
            this.context.Marcas.Remove(model);
            this.context.SaveChanges();
        }

        public List<Marca> Get()
        {
            return this.context.Marcas.ToList();
        }

        public Marca GetOne(int id)
        {
            return this.context.Marcas.Where(x => x.id == id).First();
        }

        public List<Marca> GetOnes(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Marca model)
        {
            this.context.Marcas.Update(model);
            this.context.SaveChanges();
        }
    }
}
