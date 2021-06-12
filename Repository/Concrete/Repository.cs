 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Data.Entity;
using System.Reflection;
using System.Linq.Dynamic;
using Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Repository.Concrete
{
    public class Repository<T> : IRepository<T> where T : class, new()
    {
        internal IDbContext context;
        internal DbSet<T> dbSet;

        public Repository(IDbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
        }
        public virtual IEnumerable<T> GetAll()
        {
            return dbSet.AsEnumerable<T>();
        }
        public virtual void Add(T entity)
        {
            dbSet.Add(entity);
        }
    }
}
