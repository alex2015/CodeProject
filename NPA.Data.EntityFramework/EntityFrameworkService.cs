using System;

namespace NPA.Data.EntityFramework
{
    public class EntityFrameworkService : IDataRepository, IDisposable
    {
        AngularJSDatabase _connection;

        public AngularJSDatabase dbConnection
        {
            get { return _connection; }
        }

        public void CommitTransaction(bool closeSession)
        {
            dbConnection.SaveChanges();
        }

        public void RollbackTransaction(bool closeSession)
        {
        }

        public void Save(object entity) { }
        public void CreateSession() 
        {
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<AngularJSDatabase, Configuration>()); 

            _connection = new AngularJSDatabase(); 
        }
        public void BeginTransaction() { }

        public void CloseSession() { }

        public void Dispose()
        {
            if (_connection != null)
            {
                _connection.Dispose();
            }
        }
    }
}
