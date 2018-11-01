using Microsoft.EntityFrameworkCore;

namespace DojoSecrets.Models
{
    public class DojoSecretsContext : DbContext
    {
        public DbSet<Users> users { get; set; } // always make users lowercase
        public DbSet<Secrets> secrets { get; set; } // always make users lowercase
        public DbSet<Likes> likes { get; set; } // always make users lowercase
        // base() calls the parent class' constructor passing the "options" parameter along
        public DojoSecretsContext(DbContextOptions<DojoSecretsContext> options) : base(options) { }
    }
}