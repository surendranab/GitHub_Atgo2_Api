using Atgo2.Api.CrossCuttingLayer.Logging.Interfaces;
using Atgo2.Api.DataRepository.Interfaces;
using Atgo2.Api.DataRepository.Repositories.Infrastructure;
using Atgo2.Api.Entity;
using System;
using System.Data.SqlClient;

namespace Atgo2.Api.DataRepository.Repositories
{
    public class UserRepository : Repository<ApplicationUser>, IUserRepository
    {
        private readonly IDataBaseConnection _db;
        private readonly IServiceLogger _logger;

        public UserRepository(IDataBaseConnection db, IServiceLogger logger) : base(db, logger)
        {
            _db = db;
            _logger = logger;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~UserRepository() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion


    }
}
