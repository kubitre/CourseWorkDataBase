using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace ServerDB.ServerHandlers
{
    class SendCallBack
    {
        public static void SendCallback(IAsyncResult ar)
        {
            try
            {
                var handler = (Socket)ar.AsyncState;

                int bytesSent = handler.EndSend(ar);
                Console.WriteLine($"[LOG {DateTime.Now}]: Sent {bytesSent} bytes to client.", bytesSent);

                handler.Shutdown(SocketShutdown.Both);
                handler.Close();
            }
            catch (Exception e)
            {
                ServerDb.Server.NewExceptionRequest(e.Message);
            }
        }
    }
}
