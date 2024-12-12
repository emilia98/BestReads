using BestReads.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace BestReads.Data
{
    public class DbQueryRunner : IDbQueryRunner
    {
        // OP: SHOULD IT HAVE PUBLIC SETTER
        public ApplicationDbContext Context { get; set; }

        public DbQueryRunner(ApplicationDbContext context)
        {
            this.Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Task RunQueryAsync(string query, params object[] parameters)
        {
            return this.Context.Database.ExecuteSqlRawAsync(query, parameters);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.Context?.Dispose();
            }
        }
    }
}