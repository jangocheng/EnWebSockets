﻿using System;
using System.Collections.Generic;
using System.Text;


namespace EnWebSockets.Command
{
    public class Binary : WebSocketCommandBase
    {
        public override void ExecuteCommand(WebSocket session, WebSocketCommandInfo commandInfo)
        {
            session.FireDataReceived(commandInfo.Data);
        }

        public override string Name
        {
            get { return OpCode.Binary.ToString(); }
        }
    }
}
