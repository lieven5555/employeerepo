using System.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using Repository.Model;

namespace Repository.Contexts
{
    public partial class EmployeesContext : DbContext,IDbContext
    {

        public EmployeesContext(DbContextOptions<EmployeesContext> options)
     : base(options)
        { }
        public DbSet<EmployeesDetails> EmployeesDetails { get; set; }
        public new DbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }
    }
}
