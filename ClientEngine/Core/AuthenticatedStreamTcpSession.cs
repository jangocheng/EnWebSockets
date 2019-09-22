using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Net.Security;
using System.Threading; 
using System.Threading.Tasks; 
using System.Security.Authentication; 
using System.Security.Cryptography.X509Certificates;

namespace EnWebSockets.ClientEngine
{
    public abstract class AuthenticatedStreamTcpSession : TcpClientSession
    {
        class StreamAsyncState
        {
            public AuthenticatedStream Stream { get; set; }

            public Socket Client { get; set; }

            public PosList<ArraySegment<byte>> SendingItems { get; set; }
        }

        private AuthenticatedStream m_Stream;
                

        public AuthenticatedStreamTcpSession()
            : base()
        {

        }

 
        public SecurityOption Security { get; set; }
 

        protected override void SocketEventArgsCompleted(object sender, SocketAsyncEventArgs e)
        {
            ProcessConnect(sender as Socket, null, e, null);
        }

        protected abstract void StartAuthenticatedStream(Socket client);

        protected override void OnGetSocket(SocketAsyncEventArgs e)
        {
            try
            {
                StartAuthenticatedStream(Client);
            }
            catch (Exception exc)
            {
                if (!IsIgnorableException(exc))
                    OnError(exc);
            }
        }
        
        protected void OnAuthenticatedStreamConnected(AuthenticatedStream stream)
        {
            m_Stream = stream;

            OnConnected();

            if(Buffer.Array == null)
            {
                var receiveBufferSize = ReceiveBufferSize;

                if (receiveBufferSize <= 0)
                    receiveBufferSize = DefaultReceiveBufferSize;

                ReceiveBufferSize = receiveBufferSize;

                Buffer = new ArraySegment<byte>(new byte[receiveBufferSize]);
            }

            BeginRead();
        }
         

        void BeginRead()
        { 
            ReadAsync(); 
        }
         
        private async void ReadAsync()
        {
            while (IsConnected)
            {
                var client = Client;

                if (client == null || m_Stream == null)
                    return;
                
                var buffer = Buffer;
                
                var length = 0;
                
                try
                {
                    length = await m_Stream.ReadAsync(buffer.Array, buffer.Offset, buffer.Count, CancellationToken.None);
                }
                catch (Exception e)
                {
                    if (!IsIgnorableException(e))
                        OnError(e);

                    if (EnsureSocketClosed(Client))
                        OnClosed();

                    return;
                }

                if (length == 0)
                {
                    if (EnsureSocketClosed(Client))
                        OnClosed();

                    return;
                }

                OnDataReceived(buffer.Array, buffer.Offset, length);
            }
        }
         protected override bool IsIgnorableException(Exception e)
        {
            if (base.IsIgnorableException(e))
                return true;

            if (e is System.IO.IOException)
            {
                if (e.InnerException is ObjectDisposedException)
                    return true;

                //In mono, some exception is wrapped like IOException -> IOException -> ObjectDisposedException
                if (e.InnerException is System.IO.IOException)
                {
                    if (e.InnerException.InnerException is ObjectDisposedException)
                        return true;
                }
            }

            return false;
        }
 
        protected override void SendInternal(PosList<ArraySegment<byte>> items)
        {
            SendInternalAsync(items);
        }
        
        private async void SendInternalAsync(PosList<ArraySegment<byte>> items)
        {
            try
            {
                for (int i = items.Position; i < items.Count; i++)
                {
                    var item = items[i];
                    await m_Stream.WriteAsync(item.Array, item.Offset, item.Count, CancellationToken.None);
                }
                
                m_Stream.Flush();
            }
            catch (Exception e)
            {
                if (!IsIgnorableException(e))
                    OnError(e);

                if (EnsureSocketClosed(Client))
                    OnClosed();
                    
                return;
            }
            
            OnSendingCompleted();
        }
         
        public override void Close()
        {
            var stream = m_Stream;

            if (stream != null)
            { 
                stream.Dispose();
                m_Stream = null;
            }

            base.Close();
        }
    }
}
