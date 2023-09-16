using Microsoft.EntityFrameworkCore;
using TaskManagerJob.Entities;

namespace TaskManagerJob.Data
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<UserTask>? UserTasks { get; set; }
        public DbSet<User>? Users { get; set; }
        public DbSet<Notification>? Notifications { get; set; }
    }
}
