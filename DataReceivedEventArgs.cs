﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EnWebSockets
{
    public class DataReceivedEventArgs : EventArgs
    {
        public DataReceivedEventArgs(byte[] data)
        {
            Data = data;
        }

        public byte[] Data { get; private set; }
    }
}
