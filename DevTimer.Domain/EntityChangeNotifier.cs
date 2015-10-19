using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Remoting.Channels;

namespace DevTimer.Domain
{
    internal class EntityChangeNotifier
    {
        private static readonly List<string> _connectionStrings;
        private static readonly object _lockObj = new object();

        static EntityChangeNotifier()
        {
            _connectionStrings = new List<string>();

            AppDomain.CurrentDomain.ProcessExit += (sender, e) =>
            {
                foreach (string cs in _connectionStrings)
                {
                    SqlDependency.Stop(cs);
                }
            };
        }

        internal static void AddConnectionString(string cs)
        {
            if (!_connectionStrings.Contains(cs))
            {
                lock (_lockObj)
                {
                    if (!_connectionStrings.Contains(cs))
                    {
                        SqlDependency.Start(cs);
                        _connectionStrings.Add(cs);
                    }
                }
            }
        }
    }

    public class EntityChangeNotifier<TEntity, TDbContext> : IDisposable
        where TDbContext : DbContext, new()
        where TEntity : class
    {
        private DbContext _context;
        private Expression<Func<TEntity, bool>> _query;
        private string _connectionString;

        public event EventHandler<EntityChangeEventArgs<TEntity>> Changed;
        public event EventHandler<NotifierErrorEventArgs> Error;

        public EntityChangeNotifier(Expression<Func<TEntity, bool>> query)
        {
            _context = new TDbContext();
            _query = query;
            _connectionString = _context.Database.Connection.ConnectionString;

            EntityChangeNotifier.AddConnectionString(_connectionString);

            RegisterNotification();
        }

        private void RegisterNotification()
        {
            _context = new TDbContext();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = GetCommand())
                {
                    command.Connection = connection;
                    connection.Open();

                    var sqlDependency = new SqlDependency(command);
                    sqlDependency.OnChange += _sqlDependency_OnChange;

                    command.ExecuteNonQuery();

                }
            }
        }

        private SqlCommand GetCommand()
        {
            var query = GetCurrent();
            ObjectQuery objectQuery;

            if (query is ObjectQuery)
            {
                objectQuery= query as ObjectQuery;
            }
            else
            {
                // Use this instance to create the ObjectContext
                ObjectContext objectContext = ((IObjectContextAdapter) _context).ObjectContext;

                // Use the DbSet to create the ObjectSet and get the appropriate provider.
                IQueryable queryable = objectContext.CreateObjectSet<TEntity>() as IQueryable;
                IQueryProvider provider = queryable.Provider;

                // Use the provider and expression to create the ObjectQuery.
                objectQuery = provider.CreateQuery(((IQueryable)query).Expression) as ObjectQuery;
            }

            SqlCommand command = new SqlCommand()
            {
                CommandText = objectQuery.ToTraceString()
            };

            // Add all the paramters used in query.
            foreach (ObjectParameter parameter in objectQuery.Parameters)
            {
                command.Parameters.AddWithValue(parameter.Name, parameter.Value);
            }

            return command;
        }

        private DbQuery<TEntity> GetCurrent()
        {
            var query = _context.Set<TEntity>().Where(_query) as DbQuery<TEntity>;

            return query;
        }

        private async void _sqlDependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            if (_context == null)
            {
                return;
            }

            if (e.Type == SqlNotificationType.Subscribe || e.Info == SqlNotificationInfo.Error)
            {
                var args = new NotifierErrorEventArgs
                {
                    Reason = e,
                    Sql = GetCurrent().ToString()
                };

                OnError(args);
            }
            else
            {
                var args = new EntityChangeEventArgs<TEntity>
                {
                    Results = await GetCurrent().ToListAsync(),
                    ContinueListening = true
                };

                OnChanged(args);

                if (args.ContinueListening)
                {
                    RegisterNotification();
                }
            }
        }

        protected virtual void OnChanged(EntityChangeEventArgs<TEntity> e)
        {
            if (Changed != null)
            {
                Changed(this, e);
            }
        }

        protected virtual void OnError(NotifierErrorEventArgs e)
        {
            if (Error != null)
            {
                Error(this, e);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_context != null)
                {
                    _context.Dispose();
                    _context = null;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

    public class EntityChangeEventArgs<T> : EventArgs
    {
        public IEnumerable<T> Results { get; set; } 
        public bool ContinueListening { get; set; }
    }

    public class NotifierErrorEventArgs : EventArgs
    {
        public string Sql { get; set; }
        public SqlNotificationEventArgs Reason { get; set; }
    }
}
