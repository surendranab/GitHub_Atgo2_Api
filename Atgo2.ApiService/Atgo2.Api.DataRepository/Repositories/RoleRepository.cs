using Atgo2.Api.CrossCuttingLayer.Logging.Interfaces;
using Atgo2.Api.CrossCuttingLayer.Logging.Model;
using Atgo2.Api.DataRepository.Constants;
using Atgo2.Api.DataRepository.Interfaces;
using Atgo2.Api.DataRepository.Repositories.Infrastructure;
using Atgo2.Api.Entity.Domain;
using Atgo2.Api.Entity.Model;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atgo2.Api.DataRepository.Repositories
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        private const string TableName = "Role";
        private readonly IDataBaseConnection _db;
        private readonly IServiceLogger _logger;

        public RoleRepository(IDataBaseConnection db, IServiceLogger logger) : base(db, logger)
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
        // ~RoleRepository() {
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

        public Task<List<RoleResultSet>> IsRoleAuthorized(int currentUserId, string moduleName, string permission)
        {
            _logger.Log(new LogInformation
            {
                Module = CrossCuttingLayer.Logging.Constants.RoleModule,
                UserId = currentUserId,
                Message = CrossCuttingLayer.Logging.Constants.MethodInvokedMessage
            });

            var spName = DbKeywords.StoreProcedurePrefix + TableNames.Role + CoreDbProcedures.GetByUserAuthorization;

            return Task.Factory.StartNew(() =>
            {
                var roleJson = _db.Connection.Query<RoleResultSet>(spName, new { Id = currentUserId, ModuleName = moduleName, ActionName = permission }, commandType: CommandType.StoredProcedure).ToList();
                return roleJson.ToList();
            });
        }

        public Task LogNotAuthorizedEvent(int currentUserId, string moduleName, string permission)
        {
            _logger.Log(new LogInformation
            {
                Module = CrossCuttingLayer.Logging.Constants.RoleModule,
                UserId = currentUserId,
                Message = CrossCuttingLayer.Logging.Constants.MethodInvokedMessage
            });

            // var spName = CommonKeyWords.StoreProcedurePrefix + TableNames.Role + CommonKeyWords.FindByUserAuthoriZation;

            return Task.Factory.StartNew(() =>
            {
                _db.Connection.Execute("spx_Exceptionlog_NotAuthorized", new { Id = currentUserId, ModuleName = moduleName, ActionName = permission, ExceptionType = 116, ModuleId = 117, ExceptionLogId = 0 }, commandType: CommandType.StoredProcedure);
            });
        }
    }
}
