using Microsoft.Extensions.DependencyInjection;
using Atgo2.Api.Entity;
using System;

namespace Atgo2.Api.BusinessLayer
{
    public class Services<T> : IServices<T>
    {
        private readonly AppSettings _appsettings;
        public Services(AppSettings appsettings)
        {
            _appsettings = appsettings;
        }

        public T Service =>
                //var serviceCollections = new ServiceCollection();
                //IServiceProvider serviceProvider = serviceCollections.BuildServiceProvider();
                //var serviceProvider = new ServiceCollection();
                //.AddSingleton(typeof(IDatabase<>), typeof(Database<>))
                //.AddSingleton(typeof(IServiceLogger), typeof(ServiceLogger))
                //.AddSingleton(_appsettings)
                //.AddSingleton(typeof(T))
                //.BuildServiceProvider();
                default(T); // (T)Convert.ChangeType(serviceProvider.GetService<T>(), typeof(T));
    }
}
