using System.Data.Entity;
using System.Security.Principal;
using DevTimer.Domain.Abstract;
using DevTimer.Domain.Entities;
using DevTimer.Domain.Entities.Mappings;

namespace DevTimer.Domain
{
    public class GlobalDbContext : DbContextBase
    {
        public DbSet<Client> Clients { get; set; }

        static GlobalDbContext()
        {
            Database.SetInitializer<GlobalDbContext>(null);
        }

        public GlobalDbContext(IIdentity identity) : base(identity) { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ClientMap());
        }


    }
}
