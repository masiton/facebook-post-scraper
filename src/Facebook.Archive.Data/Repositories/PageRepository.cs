using Facebook.Archive.Data.Ef;
using Facebook.Archive.Data.Model;
using Facebook.Archive.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Facebook.Archive.Data.Repositories
{
    public class PageRepository : BaseRepository<Page>
    {
        public PageRepository(FacebookDbContext context) : base(context)
        {
        }

        protected override DbSet<Page> GetDbSet()
        {
            return this.dbContext.Pages;
        }
    }
}