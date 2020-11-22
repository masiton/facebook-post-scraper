using Facebook.Archive.Data.Ef;
using Facebook.Archive.Data.Repositories;
using System;

namespace Facebook.Archive.Data.Persistence
{
    public class UnitOfWork : IDisposable
    {
        public PageRepository Pages { get; }

        public UpdateRepository Updates { get; }

        public PostRepository Posts { get; set; }

        public PostUpdateRepository PostUpdates { get; set; }

        public PostContentRepository PostContents { get; set; }

        private readonly FacebookDbContext context;

        public UnitOfWork(
            FacebookDbContext context,
            PageRepository pages,
            PostRepository posts,
            PostUpdateRepository postUpdates,
            PostContentRepository postContents,
            UpdateRepository updates)
        {
            this.context = context;
            this.Pages = pages;
            this.Posts = posts;
            this.PostUpdates = postUpdates;
            this.PostContents = postContents;
            this.Updates = updates;
        }

        public void SaveChanges()
        {
            this.context.SaveChanges();
        }

        public void Dispose()
        {
        }
    }
}