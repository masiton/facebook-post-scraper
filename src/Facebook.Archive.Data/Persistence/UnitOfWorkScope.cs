using System;

namespace Facebook.Archive.Data.Persistence
{
    public class UnitOfWorkScope : IDisposable
    {
        public UnitOfWork UnitOfWork { get; }
        
        public UnitOfWorkScope(UnitOfWork unitOfWork)
        {
            this.UnitOfWork = unitOfWork;
        }

        public void SaveChanges()
        {
            this.UnitOfWork.SaveChanges();
        }

        public void Dispose()
        {
        }
    }
}