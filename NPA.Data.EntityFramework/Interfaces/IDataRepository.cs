namespace NPA.Data.EntityFramework
{
    public interface IDataRepository
    {
        void CreateSession();
        void BeginTransaction();
        void CommitTransaction(bool closeSession);
        void RollbackTransaction(bool closeSession);
        void CloseSession();
    }
}
