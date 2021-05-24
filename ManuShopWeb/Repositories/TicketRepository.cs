using ManuShopWeb.Data;
using ManuShopWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManuShopWeb.Repositories
{
    public class TicketRepository: IRepository<Ticket, int>
    {
        Context context;

        public TicketRepository(Context context)
        {
            this.context = context;
        }

        public void Create(Ticket model)
        {
            this.context.Tickets.Add(model);
            this.context.SaveChanges();
        }

        public void Delete(Ticket model)
        {
            this.context.Tickets.Remove(model);
            this.context.SaveChanges();
        }

        public List<Ticket> Get()
        {
            return this.context.Tickets.ToList();
        }

        public Ticket GetOne(int id)
        {
            return this.context.Tickets.Where(x => x.id == id).First();
        }

        public List<Ticket> GetOnes(int id)
        {
            return this.context.Tickets.Where(x => x.tienda == id).ToList();
        }

        public void Update(Ticket model)
        {
            this.context.Tickets.Update(model);
            this.context.SaveChanges();
        }
    }
}
