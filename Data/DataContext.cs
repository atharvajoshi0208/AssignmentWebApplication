using Microsoft.EntityFrameworkCore;

namespace AssignmentWebApplication.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<User>? User { get; set; }
        public DbSet<Messages>? Messages { get; set; }
    }
}
