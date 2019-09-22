using System;
using System.Collections.Generic;
using System.Text;


namespace EnWebSockets.Command
{
    public class Text : WebSocketCommandBase
    {
        public override void ExecuteCommand(WebSocket session, WebSocketCommandInfo commandInfo)
        {
            session.FireMessageReceived(commandInfo.Text);
        }

        public override string Name
        {
            get { return OpCode.Text.ToString(); }
        }
    }
}
