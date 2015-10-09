using System;
using System.Data.Entity;
using System.Security.Principal;

namespace DevTimer.Domain.Abstract
{
    public abstract class DbContextBase : DbContext
    {
        public IIdentity _identity { get; private set; }

        public DbContextBase(IIdentity identity) : base("name=DefaultConnection")
        {
            if (identity == null)
            {
                throw new ArgumentNullException();
            }

            _identity = identity;

            ConfigureDbContext();
        }

        protected virtual void ConfigureDbContext()
        {
            Configuration.LazyLoadingEnabled = true;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Set default of non-unicode for all string column mappings
            modelBuilder.Properties<string>().Configure(c => c.IsUnicode(false));
        }
    }
}
