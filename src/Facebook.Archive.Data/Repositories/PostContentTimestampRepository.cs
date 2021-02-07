using Facebook.Archive.Data.Ef;
using Facebook.Archive.Data.Model;
using Facebook.Archive.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Facebook.Archive.Data.Repositories
{
    public class PostContentTimestampRepository : BaseRepository<PostContentTimestamp>
    {
        public PostContentTimestampRepository(FacebookDbContext context) : base(context)
        {
        }

        protected override DbSet<PostContentTimestamp> GetDbSet()
        {
            return dbContext.PostContentTimestamps;
        }
    }
}