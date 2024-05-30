using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {

        protected readonly JewelryShopDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(JewelryShopDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }
        public bool Add(T entity)
        {
            _dbSet.Add(entity);
            var result = _context.SaveChanges();
            return result > 0;
        }

        public ICollection<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public T GetById(string id)
        {
            return _dbSet.Find(id);
        }

        public bool Update(T entity)
        {
            _dbSet.Update(entity);
            var result = _context.SaveChanges();
            return result > 0;
        }
        public bool Delete(T entity)
        {
            _dbSet.Remove(entity);
            var result = _context.SaveChanges();
            return result > 0;
        }
    }
}
