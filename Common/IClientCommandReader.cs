using System;
using System.Collections.Generic;
using System.Text;

namespace EnWebSockets.Common
{
    public interface IClientCommandReader<TCommandInfo>
        where TCommandInfo : ICommandInfo
    {
        TCommandInfo GetCommandInfo(byte[] readBuffer, int offset, int length, out int left);

        IClientCommandReader<TCommandInfo> NextCommandReader { get; }
    }
}
