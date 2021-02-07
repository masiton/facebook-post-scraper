using Facebook.Archive.Data.Ef;
using Facebook.Archive.Data.Model;
using Facebook.Archive.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Facebook.Archive.Data.Repositories
{
    public class PostContentTextRepository : BaseRepository<PostContentText>
    {
        public PostContentTextRepository(FacebookDbContext context) : base(context)
        {
        }

        protected override DbSet<PostContentText> GetDbSet()
        {
            return dbContext.PostContentTexts;
        }
    }
}