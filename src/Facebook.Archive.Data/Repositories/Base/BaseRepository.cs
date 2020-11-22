using Facebook.Archive.Data.Ef;
using Facebook.Archive.Data.Model.Base;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Facebook.Archive.Data.Repositories.Base
{
    public abstract class BaseRepository<T> where T: ModelBase
    {
        protected FacebookDbContext dbContext;

        protected DbSet<T> Entities
        {
            get { return this.GetDbSet(); }
        }

        public BaseRepository(FacebookDbContext context)
        {
            this.dbContext = context;
        }

        public T FindById(object id)
        {
            return this.Entities.Find(id);
        }

        public IQueryable<T> AsNoTracking()
        {
            return this.Entities.AsNoTracking();
        }

        public IQueryable<T> AsTracking()
        {
            return this.Entities.AsQueryable();
        }

        public void Add(T entity)
        {
            this.Entities.Add(entity);
        }

        protected abstract DbSet<T> GetDbSet();
    }
}
