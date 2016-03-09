using System;

namespace NPA.Interfaces
{
   
    public interface IDataRepository
    {
        void CreateSession();
        void BeginTransaction();
        void CommitTransaction(Boolean closeSession);
        void RollbackTransaction(Boolean closeSession);
        void CloseSession();
    }

}
