using ManuShopWeb.Data;
using ManuShopWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManuShopWeb.Repositories
{
    public class FranquiciasRepository: IRepository<Franquicia, int>
    {
        Context context;
        public FranquiciasRepository(Context context)
        {
            this.context = context;
        }
        public List<Franquicia> Get()
        {
            return this.context.Franquicias.ToList();
        }
        public Franquicia GetOne(int id)
        {
            return this.context.Franquicias.Where(x => x.id == id).First();
        }
        public List<Franquicia> GetOnes(int id)
        {
            return this.context.Franquicias.Where(x => x.jefe == id).ToList();
        }
        public void Create(Franquicia Franquicia)
        {
            this.context.Franquicias.Add(Franquicia);
            this.context.SaveChanges();
        }
        public void Update(Franquicia Franquicia)
        {
            this.context.Franquicias.Update(Franquicia);
            this.context.SaveChanges();
        }
        public void Delete(Franquicia Franquicia)
        {
            this.context.Franquicias.Remove(Franquicia);
            this.context.SaveChanges();
        }
    }
}
