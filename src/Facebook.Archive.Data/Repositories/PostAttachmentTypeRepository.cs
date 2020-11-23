using Facebook.Archive.Data.Ef;
using Facebook.Archive.Data.Model;
using Facebook.Archive.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Facebook.Archive.Data.Repositories
{
    public class PostAttachmentTypeRepository : BaseRepository<PostAttachmentType>
    {
        public PostAttachmentTypeRepository(FacebookDbContext context) : base(context)
        {
        }

        protected override DbSet<PostAttachmentType> GetDbSet()
        {
            return dbContext.PostAttachmentTypes;
        }
    }
}