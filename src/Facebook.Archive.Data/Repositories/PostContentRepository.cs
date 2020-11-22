using Facebook.Archive.Data.Ef;
using Facebook.Archive.Data.Model;
using Facebook.Archive.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Facebook.Archive.Data.Repositories
{
    public class PostContentRepository : BaseRepository<PostContent>
    {
        public PostContentRepository(FacebookDbContext context) : base(context)
        {
        }

        protected override DbSet<PostContent> GetDbSet()
        {
            return this.dbContext.PostContents;
        }
    }
}
