using Facebook.Archive.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace Facebook.Archive.Data.Ef
{
    public class FacebookDbContext : DbContext
    {
        public DbSet<Page> Pages { get; set; }

        public DbSet<Update> Updates { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<PostContent> PostContents { get; set; }

        public DbSet<PostUpdate> PostUpdates { get; set; }

        public DbSet<PostAttachment> PostAttachments { get; set; }

        public DbSet<PostAttachmentType> PostAttachmentTypes { get; set; }

        public FacebookDbContext() : base()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-JO6GOO1;Initial Catalog=FacebookDB;Integrated Security=True");
        }
    }
}