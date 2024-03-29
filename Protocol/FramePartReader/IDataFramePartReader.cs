﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EnWebSockets.Protocol.FramePartReader
{
    interface IDataFramePartReader
    {
        int Process(int lastLength, WebSocketDataFrame frame, out IDataFramePartReader nextPartReader);
    }
}
