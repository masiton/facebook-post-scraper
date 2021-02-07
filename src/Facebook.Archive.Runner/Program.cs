using Facebook.Archive.Data.Ef;
using Facebook.Archive.Data.Persistence;
using Facebook.Archive.Data.Repositories;
using Facebook.Archive.Runner.Browser;
using Facebook.Archive.Runner.Handlers;
using Facebook.Archive.Runner.Managers;
using Facebook.Archive.Runner.Parsers.Page;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Facebook.Archive.Runner
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
            .AddDbContext<FacebookDbContext>()
            .AddTransient<PageRepository>()
            .AddTransient<PostContentRepository>()
            .AddTransient<PostRepository>()
            .AddTransient<PostUpdateRepository>()
            .AddTransient<UpdateRepository>()
            .AddTransient<PostContentImageRepository>()
            .AddTransient<PostContentLinkRepository>()
            .AddTransient<PostContentTextRepository>()
            .AddTransient<PostContentTimestampRepository>()
            .AddTransient<UnitOfWork>()
            .AddTransient<UnitOfWorkScopeProvider>()
            .AddTransient<FacebookPageParser>()
            .AddTransient<FacebookBrowserProvider>()
            .AddTransient<FacebookUrlHandler>()
            .AddTransient<AcquisitionManager>()
            .BuildServiceProvider();

            var manager = serviceProvider.GetService<AcquisitionManager>();

            while (true)
            {
                manager.Run().Wait();
                Task.Delay(5000).Wait();
            }
        }
    }
}
