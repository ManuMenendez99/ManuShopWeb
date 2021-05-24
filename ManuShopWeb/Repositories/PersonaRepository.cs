using ManuShopWeb.Data;
using ManuShopWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManuShopWeb.Repositories
{
    public class PersonaRepository: IRepository<Persona, int>
    {
        Context context;
        public PersonaRepository(Context context)
        {
            this.context = context;
        }

        public void Create(Persona model)
        {
            this.context.Personas.Add(model);
            this.context.SaveChanges();
        }

        public void Delete(Persona model)
        {
            this.context.Personas.Remove(model);
            this.context.SaveChanges();
        }

        public List<Persona> Get()
        {
            return this.context.Personas.ToList() ;
        }

        public Persona GetOne(int id)
        {
            return this.context.Personas.Where(x => x.id == id).First();
        }

        public List<Persona> GetOnes(int id)
        {
            return this.context.Personas.Where(x => x.tiposPersona == id).ToList();
        }

        public void Update(Persona model)
        {
            this.context.Personas.Update(model);
            this.context.SaveChanges();
        }
    }
}
