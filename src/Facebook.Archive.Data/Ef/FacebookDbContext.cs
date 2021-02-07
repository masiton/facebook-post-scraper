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

        public DbSet<PostContentImage> PostContentPhotos { get; set; }

        public DbSet<PostContentLink> PostContentUrls { get; set; }

        public DbSet<PostContentText> PostContentTexts { get; set; }

        public DbSet<PostContentTimestamp> PostContentTimestamps { get; set; }

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