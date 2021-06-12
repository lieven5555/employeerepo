
namespace Repository.Interface
{
    public interface IUnitOfWork 
    {
        void Commit();
        IRepository<T> Repository<T>() where T : class, new();
    }

}