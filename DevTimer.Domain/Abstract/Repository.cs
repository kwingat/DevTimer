using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;

namespace DevTimer.Domain.Abstract
{
    public abstract class Repository<T> : IRepository, IDisposable
        where T : class
    {
        private bool _isDisposed = false;

        protected DbSet<T> Set { get; private set; }
        protected DbContextBase Context { get; set; }

        protected Repository(DbContextBase context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("dbContect");
            }

            Context = context;
            Set = Context.Set<T>();
        }

        public virtual void Save()
        {
            Context.SaveChanges();
        }

        public virtual async Task SaveAsync()
        {
            await Context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_isDisposed)
            {
                return;
            }

            if (disposing)
            {
                Context.Dispose();
            }

            _isDisposed = true;
            Context = null;
        }

        protected void AddEntity(T entity)
        {
            if (entity is IReadOnlyEntity)
            {
                throw new InvalidOperationException();
            }

            DbEntityEntry entry = Context.Entry(entity);
            if (entry.State != EntityState.Detached)
            {
                entry.State = EntityState.Added;
            }
            else
            {
                Set.Add(entity);
            }
        }

        protected virtual void UpdateEntity(T entity)
        {
            if (entity is IReadOnlyEntity)
            {
                throw new InvalidOperationException();
            }

            DbEntityEntry entry = Context.Entry(entity);

            if (entry.State == EntityState.Detached)
            {
                Set.Attach(entity);
            }

            entry.State = EntityState.Modified;
        }

        protected virtual void DeleteEntity(T entity)
        {
            if (entity is IReadOnlyEntity)
            {
                throw new InvalidOperationException();
            }

            DbEntityEntry entry = Context.Entry(entity);

            if (entry.State != EntityState.Deleted)
            {
                entry.State = EntityState.Deleted;
            }
            else
            {
                Set.Attach(entity);
                Set.Remove(entity);
            }
        }
    }

    public interface IReadOnlyEntity
    {
        // marker interface only
    }
}
