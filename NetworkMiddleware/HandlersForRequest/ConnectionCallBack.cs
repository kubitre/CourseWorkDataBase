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
                var client = (StateObject)ar.AsyncState;
                client.WorkSocket.EndConnect(ar);
 
                client.clientNetwork.connectDone.Set();
            }
            catch (Exception e)
            {
                //(ar.AsyncState as StateObject).clientNetwork.GetExceptionHandle(e.Message);
                (ar.AsyncState as StateObject).clientNetwork.connectDone.Set();
            }
        }
    }
}
