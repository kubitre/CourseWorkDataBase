using System;
using System.Net.Sockets;

namespace AdminPanel.NetworkMiddleware.HandlersForRequest
{
    class SendCallBack
    {
        public static void SendCallback(IAsyncResult ar)
        {
            try
            {
                var client = (Socket)ar.AsyncState;
                var bytesSent = client.EndSend(ar);
                Client.sendDone.Set();
            }
            catch (Exception e)
            {
                Client.GetExceptionHandle(e.Message);
            }
        }
    }
}
