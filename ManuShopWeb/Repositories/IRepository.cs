using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManuShopWeb.Repositories
{
    public interface IRepository<T,K>
    {
        List<T> Get();
        T GetOne(K id);
        List<T> GetOnes(K id);
        void Create(T model);
        void Update(T model);
        void Delete(T model);
    }
}
