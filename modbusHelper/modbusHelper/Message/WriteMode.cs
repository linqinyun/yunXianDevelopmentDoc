using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace modbusHelper.Message
{
    internal enum WriteMode
    {
        Write01 = 0x05,
        Write03 = 0x06,
        Write01s = 0x0F,
        Write03s = 0x10
    }
}
