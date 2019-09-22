using System;
using System.Collections.Generic;
using System.Text;

namespace EnWebSockets.ClientEngine
{
    public interface IBufferSetter
    {
        void SetBuffer(ArraySegment<byte> bufferSegment);
    }
}
