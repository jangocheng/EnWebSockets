using System;
using System.Collections.Generic;
using System.Text;

namespace WebSocket4Net.ClientEngine
{
    public class ErrorEventArgs : EventArgs
    {
        public Exception Exception { get; private set; }

        public ErrorEventArgs(Exception exception)
        {
            Exception = exception;
        }
    }
}
