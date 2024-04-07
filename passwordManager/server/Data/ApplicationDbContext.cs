using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using server.Models;

namespace server.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Password> Password { get; set; }
    }
}
