using Facebook.Archive.Data.Ef;
using Facebook.Archive.Data.Model;
using Facebook.Archive.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Facebook.Archive.Data.Repositories
{
    public class PostContentPhotoRepository : BaseRepository<PostContentPhoto>
    {
        public PostContentPhotoRepository(FacebookDbContext context) : base(context)
        {
        }

        protected override DbSet<PostContentPhoto> GetDbSet()
        {
            return dbContext.PostContentPhotos;
        }
    }
}