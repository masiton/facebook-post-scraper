namespace Facebook.Archive.Data.Persistence
{
    public class UnitOfWorkScopeProvider
    {
        private UnitOfWork unitOfWork;

        public UnitOfWorkScopeProvider(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public UnitOfWorkScope GetScope()
        {
            return new UnitOfWorkScope(this.unitOfWork);
        }
    }
}