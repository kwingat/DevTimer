using System.Data.Entity;
using System.Security.Principal;
using DevTimer.Domain.Abstract;
using DevTimer.Domain.Entities;
using DevTimer.Domain.Entities.Mappings;

namespace DevTimer.Domain
{
    public class GlobalDbContext : DbContextBase
    {
        public DbSet<AspNetUser> AspNetUsers { get; set; } 
        public DbSet<Client> Clients { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Worker> Workers { get; set; }
        public DbSet<WorkerType> WorkerTypes { get; set; } 
        public DbSet<Work> Works { get; set; }
        public DbSet<WorkType> WorkTypes { get; set; }

        static GlobalDbContext()
        {
            Database.SetInitializer<GlobalDbContext>(null);
        }

        public GlobalDbContext(IIdentity identity) : base(identity) { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AspNetUserMap());
            modelBuilder.Configurations.Add(new ClientMap());
            modelBuilder.Configurations.Add(new ProjectMap());
            modelBuilder.Configurations.Add(new StateMap());
            modelBuilder.Configurations.Add(new WorkerMap());
            modelBuilder.Configurations.Add(new WorkerTypeMap());
            modelBuilder.Configurations.Add(new WorkMap());
            modelBuilder.Configurations.Add(new WorkTypeMap());
        }
    }
}
