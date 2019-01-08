using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Linq.Expressions;

namespace DocHub.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {

        protected Models.DBContext Context = null;
        public GenericRepository(Models.DBContext Context)
        {
            this.Context = Context;
        }

        public void Add(T obj)
        {
            try
            {
                Context.Set<T>().Add(obj);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public void Delete(T obj)
        {
            try
            {
                Context.Set<T>().Attach(obj);
                Context.Entry(obj).State = System.Data.Entity.EntityState.Deleted;
                Context.Set<T>().Remove(obj);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(T obj)
        {
            try
            {
                Context.Set<T>().Attach(obj);
                Context.Entry(obj).State = System.Data.Entity.EntityState.Modified;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }

        public IEnumerable<T> SelectAll()
        {
            return Context.Set<T>();
        }

        public T Find(Expression<Func<T, bool>> predicate)
        {
            return Context.Set<T>().Where(predicate).DefaultIfEmpty(null).FirstOrDefault();
        }

        public IEnumerable<T> FindList(Expression<Func<T, bool>> predicate)
        {
            return Context.Set<T>().Where(predicate).ToList();
        }

    }

}