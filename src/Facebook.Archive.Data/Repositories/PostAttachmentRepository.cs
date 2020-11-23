using Facebook.Archive.Data.Ef;
using Facebook.Archive.Data.Model;
using Facebook.Archive.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Facebook.Archive.Data.Repositories
{
    public class PostAttachmentRepository : BaseRepository<PostAttachment>
    {
        public PostAttachmentRepository(FacebookDbContext context) : base(context)
        {
        }

        protected override DbSet<PostAttachment> GetDbSet()
        {
            return dbContext.PostAttachments;
        }
    }
}