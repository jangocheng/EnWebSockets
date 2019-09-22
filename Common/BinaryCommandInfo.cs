using System;
using System.Collections.Generic;
using System.Text;

namespace EnWebSockets.Common
{
    public class BinaryCommandInfo : CommandInfo<byte[]>
    {
        public BinaryCommandInfo(string key, byte[] data)
            : base(key, data)
        {

        }
    }
}
