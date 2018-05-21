using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XcBIMwebSys.Service.Interface;

namespace XcBIMwebSys.Service.Implement
{
    public partial class TestService : ITestService
    {

        public TestService()
        {
        }


        public string GetCurrentTime()
        {
            return System.DateTime.Now.ToString();
        }



    }
}
