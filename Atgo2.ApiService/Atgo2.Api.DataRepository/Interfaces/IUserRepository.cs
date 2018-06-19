using Atgo2.Api.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Atgo2.Api.DataRepository.Interfaces
{
    public interface IUserRepository : IRepository<ApplicationUser>, IDisposable
    {

    }
}
