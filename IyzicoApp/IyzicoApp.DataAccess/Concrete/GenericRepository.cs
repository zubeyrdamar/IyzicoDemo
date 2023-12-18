using IyzicoApp.DataAccess.Abstract;
using Microsoft.EntityFrameworkCore;

namespace IyzicoApp.DataAccess.Concrete
{
    public class GenericRepository<TEntity, TContext> : IRepository<TEntity> where TEntity : class where TContext : DbContext, new()
    {
        public IEnumerable<TEntity> List()
        {
            using (var context = new TContext())
            {
                return context.Set<TEntity>().ToList();
            }
        }

        public void Create(TEntity entity)
        {
            using (var context = new TContext())
            {
                context.Set<TEntity>().Add(entity);
                context.SaveChanges();
            }
        }

        public TEntity Read(Guid id)
        {
            using (var context = new TContext())
            {
                return context.Set<TEntity>().Find(id);
            }
        }

        public void Update(TEntity entity)
        {
            using (var context = new TContext())
            {
                context.Entry(entity).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void Delete(TEntity entity)
        {
            using (var context = new TContext())
            {
                context.Set<TEntity>().Remove(entity);
                context.SaveChanges();
            }
        }
    }
}
