using Microsoft.EntityFrameworkCore;

namespace Repository.Interface
{
    public interface IDbContext
    {
        DbSet<T> Set<T>() where T : class;
        int SaveChanges();
    }

}
