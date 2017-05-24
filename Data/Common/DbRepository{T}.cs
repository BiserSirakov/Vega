namespace Vega.Data.Common
{
    using System;
    using System.Linq;

    using Microsoft.EntityFrameworkCore;

    using Models;

    public class DbRepository<T> : IDbRepository<T>
        where T : BaseModel
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _dbSet;

        public DbRepository(DbContext context)
        {
            _context = context ?? throw new ArgumentException("An instance of DbContext is required to use this repository.", nameof(context));
            _dbSet = _context.Set<T>();
        }

        public IQueryable<T> GetAll(Func<IQueryable<T>, IQueryable<T>> func = null, bool withDeleted = false)
        {
            var all = withDeleted ? _dbSet : _dbSet.Where(x => !x.IsDeleted);
            return func == null ? all : func(all);
        }

        public T GetById(int id, Func<IQueryable<T>, IQueryable<T>> func = null)
        {
            return func == null ? this.GetAll().FirstOrDefault(x => x.Id == id) : this.GetAll(func: func).FirstOrDefault(x => x.Id == id);
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Delete(T entity, bool hardDelete = false)
        {
            if (hardDelete)
            {
                _dbSet.Remove(entity);
            }
            else
            {
                entity.IsDeleted = true;
                entity.DeletedOn = DateTime.Now;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}