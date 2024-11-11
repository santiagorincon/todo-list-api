using Microsoft.EntityFrameworkCore;
using todo_list.DataAccess.Models;

namespace todo_list.DataAccess
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() { }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public virtual DbSet<TaskItem> Tasks { get; set; }
    }
}
