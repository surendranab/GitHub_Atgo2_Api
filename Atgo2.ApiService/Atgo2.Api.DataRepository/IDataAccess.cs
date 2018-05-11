using System;
using System.Collections.Generic;
using System.Text;

namespace Atgo2.Api.DataRepository
{
    public interface IDatabase<out T> : IDisposable
    {
        T Repository { get; }
    }
}
