using Facebook.Archive.Data.Ef;
using Facebook.Archive.Data.Model;
using Facebook.Archive.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Facebook.Archive.Data.Repositories
{
    public class PostContentLinkRepository : BaseRepository<PostContentLink>
    {
        public PostContentLinkRepository(FacebookDbContext context) : base(context)
        {
        }

        protected override DbSet<PostContentLink> GetDbSet()
        {
            return dbContext.PostContentUrls;
        }
    }
}