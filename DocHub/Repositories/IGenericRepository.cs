using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace DocHub.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        void Add(T obj);
        void Delete(T obj);
        IEnumerable<T> SelectAll();
        T Find(Expression<Func<T, bool>> predicate);
        IEnumerable<T> FindList(Expression<Func<T, bool>> predicate);
        void Update(T obj);

    }
}