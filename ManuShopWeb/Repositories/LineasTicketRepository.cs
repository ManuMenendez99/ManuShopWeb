using ManuShopWeb.Data;
using ManuShopWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManuShopWeb.Repositories
{
    public class LineasTicketRepository : IRepository<LineasTicket, int>
    {
        Context context;
        public LineasTicketRepository(Context context)
        {
            this.context = context;
        }
        public void Create(LineasTicket model)
        {
            this.context.LineasTickets.Add(model);
            this.context.SaveChanges();
        }

        public void Delete(LineasTicket model)
        {
            this.context.LineasTickets.Remove(model);
            this.context.SaveChanges();
        }

        public List<LineasTicket> Get()
        {
            return this.context.LineasTickets.ToList();
        }

        public LineasTicket GetOne(int id)
        {
            return this.context.LineasTickets.Where(x => x.id == id).First();
        }

        public List<LineasTicket> GetOnes(int id)
        {
            return this.context.LineasTickets.Where(x => x.ticket == id).ToList();
        }

        public void Update(LineasTicket model)
        {
            this.context.LineasTickets.Update(model);
            this.context.SaveChanges();
        }
    }
}
