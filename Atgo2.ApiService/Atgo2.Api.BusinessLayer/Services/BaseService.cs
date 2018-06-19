using Atgo2.Api.CrossCuttingLayer.Logging.Interfaces;
using Atgo2.Api.DataRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Atgo2.Api.BusinessLayer
{
    public abstract class BaseService<TRepository, TEntity>
    {
        private readonly IDatabase<TRepository> _repository;
        private readonly IServiceLogger _logger;

        protected BaseService(IDatabase<TRepository> repository, IServiceLogger logger)
        {
            _repository = repository;
            _logger = logger;
        }
    }
}
