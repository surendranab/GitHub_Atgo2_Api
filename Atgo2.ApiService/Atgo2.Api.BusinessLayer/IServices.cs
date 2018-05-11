using System;
using System.Collections.Generic;
using System.Text;

namespace Atgo2.Api.BusinessLayer
{
    public interface IServices<out T>
    {
        T Service { get; }
    }
}
