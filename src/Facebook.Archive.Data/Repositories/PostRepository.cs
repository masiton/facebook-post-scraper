using Facebook.Archive.Data.Ef;
using Facebook.Archive.Data.Model;
using Facebook.Archive.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Facebook.Archive.Data.Repositories
{
    public class PostRepository : BaseRepository<Post>
    {
        public PostRepository(FacebookDbContext context) : base(context)
        {
        }

        protected override DbSet<Post> GetDbSet()
        {
            return this.dbContext.Posts;
        }
    }
}
