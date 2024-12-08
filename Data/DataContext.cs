using Microsoft.EntityFrameworkCore;

namespace SimpleCRUD.Data;

public class DataContext: DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }

    public DbSet<Models.Employee> Employees { get; set; }
    public DbSet<Models.Category> Categories { get; set; }
}
