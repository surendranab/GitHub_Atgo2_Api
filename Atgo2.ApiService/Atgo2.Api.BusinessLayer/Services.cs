using Microsoft.Extensions.DependencyInjection;
using Atgo2.Api.Entity;
using System;
using Atgo2.Api.DataRepository;
using Atgo2.Api.CrossCuttingLayer.Logging.Interfaces;
using Atgo2.Api.CrossCuttingLayer.Logging;

namespace Atgo2.Api.BusinessLayer
{
    public class Services<T> : IServices<T>
    {
        private readonly AppSettings _appsettings;
        public Services(AppSettings appsettings)
        {
            _appsettings = appsettings;
        }

        public T Service
        {
            get
            {
                //var serviceCollections = new ServiceCollection()
                //IServiceProvider serviceProvider = serviceCollections.BuildServiceProvider();
                var serviceProvider = new ServiceCollection()
                //.AddSingleton(typeof(IDatabase<>), typeof(Database<>))
                .AddSingleton(typeof(IServiceLogger), typeof(ServiceLogger))
                .AddSingleton(_appsettings)
                .AddSingleton(typeof(T))
                .BuildServiceProvider();
                return (T)Convert.ChangeType(serviceProvider.GetService<T>(), typeof(T));
                // default(T);
            }
        }
    }
}
