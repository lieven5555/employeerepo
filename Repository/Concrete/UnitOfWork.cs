


using Repository.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Concrete
{
    public class UnitOfWork:IUnitOfWork
    {
        #region fields
        protected  IDbContext _context;
        protected Hashtable _repositories;
        #endregion
        public UnitOfWork(IDbContext context)
        {
            _context = context;
        }
        public void Commit()
        {
            _context.SaveChanges();
        }
        public IRepository<T> Repository<T>() where T : class, new()
        {
            if (_repositories == null)
                _repositories = new Hashtable();

            var type = typeof(T).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(Repository<>);

                var repositoryInstance =
                    Activator.CreateInstance(repositoryType
                            .MakeGenericType(typeof(T)), _context);

                _repositories.Add(type, repositoryInstance);
            }
            return (IRepository<T>)_repositories[type];
        }
    }
}
