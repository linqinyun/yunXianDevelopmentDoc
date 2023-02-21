using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace modbusHelper.Message
{
    internal enum ReadMode
    {
        Read01 = 0x01,
        Read02 = 0x02,
        Read03 = 0x03,
        Read04 = 0x04,
    }
}
