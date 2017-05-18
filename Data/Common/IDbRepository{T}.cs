namespace Vega.Data.Common
{
    using System;
    using System.Linq;

    using Models;

    public interface IDbRepository<T>
        where T : BaseModel
    {
        IQueryable<T> GetAll(bool withDeleted = false, Func<IQueryable<T>, IQueryable<T>> func = null);

        T GetById(int id, Func<IQueryable<T>, IQueryable<T>> func = null);

        void Add(T entity);

        void Delete(T entity, bool hardDelete = false);

        void Save();
    }
}