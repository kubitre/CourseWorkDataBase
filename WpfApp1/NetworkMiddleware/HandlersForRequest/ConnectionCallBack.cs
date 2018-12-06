using System;
using System.Net.Sockets;

namespace AdminPanel.NetworkMiddleware.HandlersForRequest
{
    class ConnectionCallBack
    {
        public static void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                var client = (Socket)ar.AsyncState;
                client.EndConnect(ar);
 
                Client.connectDone.Set();
            }
            catch (Exception e)
            {
                Client.GetExceptionHandle(e.Message);
                Client.connectDone.Set();
            }
        }
    }
}
