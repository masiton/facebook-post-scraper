using Facebook.Archive.Data.Ef;
using Facebook.Archive.Data.Model;
using Facebook.Archive.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Facebook.Archive.Data.Repositories
{
    public class PostContentImageRepository : BaseRepository<PostContentImage>
    {
        public PostContentImageRepository(FacebookDbContext context) : base(context)
        {
        }

        protected override DbSet<PostContentImage> GetDbSet()
        {
            return dbContext.PostContentPhotos;
        }
    }
}