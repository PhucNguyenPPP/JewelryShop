using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DAO
{
    public interface IGenericDAO <T> where T : class
    {
        void Add(T entity);
        void Update(T entity);
        IQueryable<T> GetAll(Expression<Func<T, bool>> expression);
        T GetById(Guid id);
        void Delete(T entity);
        void AddRange(List<T> entities);
        void UpdateRange(List<T> entities);
        void DeleteRange(List<T> entities);
        bool SaveChange();
    }
}
