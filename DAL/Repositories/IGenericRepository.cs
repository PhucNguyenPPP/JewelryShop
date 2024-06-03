using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        void Add(T entity);
        void Update(T entity);
        ICollection<T> GetAll();
        T GetById(Guid id);
        void Delete(T entity);
        void AddRange (List<T> entities);
        void UpdateRange (List<T> entities);
        void DeleteRange(List<T> entities);
        bool SaveChange();

    }
}
