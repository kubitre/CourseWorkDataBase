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
                var client = (StateObject)ar.AsyncState;
                var bytesSent = client.WorkSocket.EndSend(ar);

                (ar.AsyncState as StateObject).clientNetwork.sendDone.Set();
            }
            catch (Exception e)
            {
                (ar.AsyncState as StateObject).clientNetwork.GetExceptionHandle(e.Message);
            }
        }
    }
}
