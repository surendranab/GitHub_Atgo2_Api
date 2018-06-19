using Atgo2.Api.CrossCuttingLayer.Logging.Interfaces;
using Atgo2.Api.CrossCuttingLayer.Logging.Model;
using Atgo2.Api.DataRepository.Constants;
using Atgo2.Api.DataRepository.Interfaces;
using Atgo2.Api.DataRepository.Repositories.Infrastructure;
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
    public abstract class Repository<TEntity> : IRepository<TEntity>
    {
        private readonly IDataBaseConnection _db;
        private readonly IServiceLogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{T}" /> class.
        /// </summary>
        /// <param name="db">db db.</param>
        /// <param name="logger"></param>
        protected Repository(IDataBaseConnection db, IServiceLogger logger)
        {
            _db = db;
            _logger = logger;
        }

        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="currentUserId"></param>
        public Task<BaseModel> Insert(TEntity item, int currentUserId)
        {
            _logger.Log(new LogInformation
            {
                Module = typeof(TEntity).Name,
                UserId = currentUserId,
                Message = CrossCuttingLayer.Logging.Constants.MethodInvokedMessage
            });

            var spName = DbKeywords.StoreProcedurePrefix + typeof(TEntity).Name + CommonDbOperations.Insert;

            if (item == null)
                throw new ArgumentNullException(typeof(TEntity).Name);

            var parameters = (object) ModelMapper.Mapping(item, currentUserId);

            return
                Task.Factory.StartNew(
                    () =>
                        _db.Connection.Query<BaseModel>(spName, parameters, commandType: CommandType.StoredProcedure).SingleOrDefault());
        }

        /// <summary>
        /// Updates the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="currentUserId"></param>
        public Task<BaseModel> Update(TEntity item, int currentUserId)
        {
            _logger.Log(new LogInformation
            {
                Module = typeof(TEntity).Name,
                UserId = currentUserId,
                Message = CrossCuttingLayer.Logging.Constants.MethodInvokedMessage
            });

            var spName = DbKeywords.StoreProcedurePrefix + typeof(TEntity).Name + CommonDbOperations.Insert;

            if (item == null)
                throw new ArgumentNullException(typeof(TEntity).Name);

            var parameters = (object) ModelMapper.Mapping(item, currentUserId);

            //return Task.Factory.StartNew(() =>
            //{
            //    _db.Connection.Execute(spName, parameters, commandType: CommandType.StoredProcedure);
            //});
            return
              Task.Factory.StartNew(
                  () =>
                      _db.Connection.Query<BaseModel>(spName, parameters, commandType: CommandType.StoredProcedure).FirstOrDefault());

        }

        /// <summary>
        /// Removes the specified item.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="currentUserId"></param>
        public Task Delete(int id, int currentUserId)
        {
            _logger.Log(new LogInformation
            {
                Module = typeof(TEntity).Name,
                UserId = currentUserId,
                Message = CrossCuttingLayer.Logging.Constants.MethodInvokedMessage
            });

            var spName = DbKeywords.StoreProcedurePrefix + typeof(TEntity).Name + CommonDbOperations.Delete;

            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id), $"'{new { id }}' is not a valid ID.");

            return Task.Factory.StartNew(() =>
            {
                _db.Connection.Execute(spName, new { Id = id }, commandType: CommandType.StoredProcedure);
            });
        }

        /// <summary>
        /// Finds by ID.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="currentUserId"></param>
        /// <returns></returns>
        public Task<TEntity> FindById(int id, int currentUserId)
        {
            _logger.Log(new LogInformation
            {
                Module = typeof(TEntity).Name,
                UserId = currentUserId,
                Message = CrossCuttingLayer.Logging.Constants.MethodInvokedMessage
            });

            var spName = DbKeywords.StoreProcedurePrefix + typeof(TEntity).Name + CommonDbOperations.GetById;

            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id), $"'{new { id }}' is not a valid ID.");

            return Task.Factory.StartNew(() => _db.Connection.Query<TEntity>(spName, new { Id = id, CurrentUserId = currentUserId }, commandType: CommandType.StoredProcedure).SingleOrDefault());
        }

        /// <summary>
        /// Finds all.
        /// </summary>
        /// <returns>All items</returns>
        public Task<IEnumerable<TEntity>> FindAll(int currentUserId)
        {
            _logger.Log(new LogInformation
            {
                Module = typeof(TEntity).Name,
                UserId = currentUserId,
                Message = CrossCuttingLayer.Logging.Constants.MethodInvokedMessage
            });

            var spName = DbKeywords.StoreProcedurePrefix + typeof(TEntity).Name + CommonDbOperations.GetAll;
            return Task.Factory.StartNew(() => _db.Connection.Query<TEntity>(spName, new { CurrentUserId = currentUserId }, commandType: CommandType.StoredProcedure));
        }

        /// <summary>
        /// Find All Grid Data
        /// </summary>
        /// <param name="isActive"></param>
        /// <param name="searchText"></param>
        /// <param name="sortColumn"></param>
        /// <param name="sortBy"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="currentUserId"></param>
        /// <returns></returns>
        public Task<IEnumerable<TEntity>> FindAllGridData(bool isActive, string searchText, string sortColumn, string sortBy, int pageNumber, int pageSize, int currentUserId)
        {
            _logger.Log(new LogInformation
            {
                Module = typeof(TEntity).Name,
                UserId = currentUserId,
                Message = CrossCuttingLayer.Logging.Constants.MethodInvokedMessage
            });

            var spName = DbKeywords.StoreProcedurePrefix + typeof(TEntity).Name + CommonDbOperations.Select;
            return Task.Factory.StartNew(() => _db.Connection.Query<TEntity>(spName, new { isActive, searchText, sortColumn, sortBy, pageNumber, pageSize, CurrentUserId = currentUserId }, commandType: CommandType.StoredProcedure));
        }
    }
}
