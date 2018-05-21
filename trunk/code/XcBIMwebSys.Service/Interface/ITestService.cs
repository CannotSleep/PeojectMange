using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tmp.Core.Dependency;

namespace XcBIMwebSys.Service.Interface
{
    public interface ITestService : IDependency
    {
        string GetCurrentTime();
    }
}
