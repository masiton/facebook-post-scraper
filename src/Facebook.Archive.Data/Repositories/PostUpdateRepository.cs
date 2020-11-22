using Facebook.Archive.Data.Ef;
using Facebook.Archive.Data.Model;
using Facebook.Archive.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Facebook.Archive.Data.Repositories
{
    public class PostUpdateRepository : BaseRepository<PostUpdate>
    {
        public PostUpdateRepository(FacebookDbContext context) : base(context)
        {
        }

        protected override DbSet<PostUpdate> GetDbSet()
        {
            return this.dbContext.PostUpdates;
        }
    }
}
