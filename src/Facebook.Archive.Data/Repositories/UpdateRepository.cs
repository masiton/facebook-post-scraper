using Facebook.Archive.Data.Ef;
using Facebook.Archive.Data.Model;
using Facebook.Archive.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Facebook.Archive.Data.Repositories
{
    public class UpdateRepository : BaseRepository<Update>
    {
        public UpdateRepository(FacebookDbContext context) : base(context)
        {
        }

        protected override DbSet<Update> GetDbSet()
        {
            return dbContext.Updates;
        }
    }
}