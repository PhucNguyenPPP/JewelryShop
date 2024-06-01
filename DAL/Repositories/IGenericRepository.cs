using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        bool Add(T entity);
        bool Update(T entity);
        ICollection<T> GetAll();
        T GetById(Guid id);
        bool Delete(T entity);
    }
}
