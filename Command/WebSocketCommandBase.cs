using System;
using System.Collections.Generic;
using System.Text;

using EnWebSockets.Common;

namespace EnWebSockets.Command
{
    public abstract class WebSocketCommandBase : ICommand<WebSocket, WebSocketCommandInfo>
    {
        public abstract void ExecuteCommand(WebSocket session, WebSocketCommandInfo commandInfo);

        public abstract string Name { get; }
    }
}
