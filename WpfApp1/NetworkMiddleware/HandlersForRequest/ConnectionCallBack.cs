using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

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
                Console.WriteLine(e.ToString());
                Client.connectDone.Set();

            }
        }
    }
}
