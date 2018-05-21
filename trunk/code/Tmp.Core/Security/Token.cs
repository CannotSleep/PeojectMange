using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tmp.Core.Security
{
    public class Token
    {
        public static string createToken()
        {
            Random ro = new Random(10);
            long tick = DateTime.Now.Ticks;
            Random ran = new Random((int)(tick & 0xffffffffL) | (int)(tick >> 32));
            return ran.Next().ToString();
        }
    }
}
