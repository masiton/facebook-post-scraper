using Facebook.Archive.Data.Ef;
using Facebook.Archive.Data.Model;
using Facebook.Archive.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Facebook.Archive.Data.Repositories
{
    public class PostContentUrlRepository : BaseRepository<PostContentUrl>
    {
        public PostContentUrlRepository(FacebookDbContext context) : base(context)
        {
        }

        protected override DbSet<PostContentUrl> GetDbSet()
        {
            return dbContext.PostContentUrls;
        }
    }
}