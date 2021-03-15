using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HJ001.HandleService
{
    public interface IScopedService
    {
        string MyName { get; set; }

        string GetScopedName();
    }
}
