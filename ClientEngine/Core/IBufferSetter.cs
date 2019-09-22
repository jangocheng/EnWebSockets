using System;
using System.Collections.Generic;
using System.Text;

namespace WebSocket4Net.ClientEngine
{
    public interface IBufferSetter
    {
        void SetBuffer(ArraySegment<byte> bufferSegment);
    }
}
