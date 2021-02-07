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

        public PostContentPhotoRepository PostContentPhotos { get; set; }

        public PostContentUrlRepository PostContentUrls { get; set; }

        public PostContentTextRepository PostContentTexts { get; set; }

        public PostContentTimestampRepository PostContentTimestamps { get; set; }

        private readonly FacebookDbContext context;

        public UnitOfWork(
            FacebookDbContext context,
            PageRepository pages,
            PostRepository posts,
            PostUpdateRepository postUpdates,
            PostContentRepository postContents,
            UpdateRepository updates,
            PostContentPhotoRepository postContentPhotos,
            PostContentTextRepository postContentTexts,
            PostContentUrlRepository postContentUrls,
            PostContentTimestampRepository postContentTimestamps)
        {
            this.context = context;
            this.Pages = pages;
            this.Posts = posts;
            this.PostUpdates = postUpdates;
            this.PostContents = postContents;
            this.Updates = updates;
            this.PostContentPhotos = postContentPhotos;
            this.PostContentUrls = postContentUrls;
            this.PostContentTexts = postContentTexts;
            this.PostContentTimestamps = postContentTimestamps;
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