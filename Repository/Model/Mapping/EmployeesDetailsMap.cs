using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Model.Mapping
{
    public class EmployeesDetailsMap 
    {
        public EmployeesDetailsMap(EntityTypeBuilder<EmployeesDetails> entityBuilder)
        {
            entityBuilder.HasKey(a => a.ID);
            entityBuilder.Property(a => a.FirstName);
            entityBuilder.Property(a => a.LastName);
            entityBuilder.Property(a => a.Age);
        }
    }
}
