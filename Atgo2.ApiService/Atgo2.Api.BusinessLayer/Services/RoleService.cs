using Atgo2.Api.BusinessLayer.Interface;
using Atgo2.Api.CrossCuttingLayer.Logging;
using Atgo2.Api.CrossCuttingLayer.Logging.Interfaces;
using Atgo2.Api.CrossCuttingLayer.Logging.Model;
using Atgo2.Api.DataRepository;
using Atgo2.Api.DataRepository.Repositories;
using Atgo2.Api.Entity.Domain;
using Atgo2.Api.Entity.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Atgo2.Api.BusinessLayer.Services
{
    public class RoleService : BaseService<RoleRepository, Role>, IRoleService
    {
        private readonly IDatabase<RoleRepository> _role;
        private readonly IServiceLogger _logger;
        public RoleService(IDatabase<RoleRepository> role, IServiceLogger logger) : base(role, logger)
        {
            _role = role;
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
                    _role.Repository?.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~RoleService() {
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

        public virtual async Task<List<RoleResultSet>> IsRoleAuthorized(int currentUserId, string moduleName, string permission)
        {
            _logger.Log(new LogInformation
            {
                Module = Constants.RoleModule,
                UserId = currentUserId,
                Message = Constants.MethodInvokedMessage
            });

            try
            {
                var roles = await _role.Repository.IsRoleAuthorized(currentUserId, moduleName, permission);
                if (roles != null)
                    return roles;

                return await Task.FromResult<List<RoleResultSet>>(null);
            }
            catch (Exception exception)
            {
                _logger.Log(new LogInformation
                {
                    Data = $"currentUserId: {currentUserId}",
                    Module = Constants.RoleModule,
                    UserId = currentUserId,
                    Exception = exception,
                    Message = Constants.ExceptionMessage
                });
                throw;
            }
        }
        #endregion

        //public void Dispose()
        //{
        //    Dispose(true);
        //    GC.SuppressFinalize(this);
        //}

        //protected virtual void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        _role.Repository?.Dispose();
        //    }
        //}

        public virtual async Task<List<RoleResultSet>> LogNotAuthorizedEvent(int currentUserId, string moduleName, string permission)
        {
            _logger.Log(new LogInformation
            {
                Module = Constants.RoleModule,
                UserId = currentUserId,
                Message = Constants.MethodInvokedMessage
            });

            try
            {
                await _role.Repository.LogNotAuthorizedEvent(currentUserId, moduleName, permission);


                return await Task.FromResult<List<RoleResultSet>>(null);
            }
            catch (Exception exception)
            {
                _logger.Log(new LogInformation
                {
                    Data = $"currentUserId: {currentUserId}",
                    Module = Constants.RoleModule,
                    UserId = currentUserId,
                    Exception = exception,
                    Message = Constants.ExceptionMessage
                });
                throw;
            }
        }
    }
}
