using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HJ001.HandleService
{
    public class ScopedTwoService : IScopedService
    {
        public string MyName { get; set; }

        public string GetScopedName()
        {
            return MyName + "第二个";
        }
    }
}
