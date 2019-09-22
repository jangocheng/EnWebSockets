using System;
using System.Collections.Generic;
using System.Text;

namespace EnWebSockets.ClientEngine
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
