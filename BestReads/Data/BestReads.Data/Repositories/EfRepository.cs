using BestReads.Data.Common.Models;
using BestReads.Data.Common.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BestReads.Data.Repositories
{
    public class EfRepository<TEntity> : IRepository<TEntity>
        where TEntity : BaseModel<int>
    {
        public EfRepository(ApplicationDbContext dbContext)
        {
            this.DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            this.DbSet = this.DbContext.Set<TEntity>();
        }

        protected ApplicationDbContext DbContext { get; private set; }

        protected DbSet<TEntity> DbSet { get; private set; }

        public virtual IQueryable<TEntity> All() => this.DbSet;

        public virtual IQueryable<TEntity> AllAsNoTracking() => this.DbSet.AsNoTracking();

        public virtual Task AddAsync(TEntity entity) => this.DbSet.AddAsync(entity).AsTask();

        public virtual void Update(TEntity entity)
        {
            var entry = this.DbContext.Entry(entity);

            if (entry.State == EntityState.Detached)
            { this.DbSet.Attach(entity);
                
            }

            entry.State = EntityState.Modified;
        }

        public virtual void Delete(TEntity entity) => this.DbSet.Remove(entity);

        public Task<int> SaveChangesAsync() => this.DbContext.SaveChangesAsync();

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
        
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.DbContext?.Dispose();
            }
        }

        public async Task<TEntity?> GetById(int id)
        {
            var dbSet = this.DbContext.Set<TEntity>();
            return await dbSet.FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}