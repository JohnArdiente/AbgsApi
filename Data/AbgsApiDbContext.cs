using AbgsApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AbgsApi.Data
{
    public class AbgsApiDbContext : DbContext
    {
        public AbgsApiDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
