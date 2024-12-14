using BestReads.Data.Common.Models;
using BestReads.Data.Common.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BestReads.Data.Repositories
{
    public class EfDeletableEntityRepository<TEntity> : EfRepository<TEntity>, IDeletableEntityRepository<TEntity>
        where TEntity : BaseDeletableModel<int>
    {
        public EfDeletableEntityRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }

        public override IQueryable<TEntity> All() => base.All().Where(e => !e.IsDeleted);

        public override IQueryable<TEntity> AllAsNoTracking() => base.AllAsNoTracking().Where(e => !e.IsDeleted);

        public IQueryable<TEntity> AllWithDeleted() => base.All().IgnoreQueryFilters();

        public IQueryable<TEntity> AllAsNoTrackingWithDeleted() => base.AllAsNoTracking().IgnoreQueryFilters();

        public void HardDelete(TEntity entity) => base.Delete(entity);

        public void Undelete(TEntity entity)
        {
            entity.IsDeleted = false;
            entity.DeletedAt = null;
            this.Update(entity);
        }

        public override void Delete(TEntity entity)
        {
            entity.IsDeleted = true;
            entity.DeletedAt = DateTime.UtcNow;
            this.Update(entity);
        }
    }
}