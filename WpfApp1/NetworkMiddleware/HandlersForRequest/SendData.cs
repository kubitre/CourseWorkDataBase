using System;
using System.Net.Sockets;
using System.Text;

namespace AdminPanel.NetworkMiddleware.HandlersForRequest
{
    class SendData
    {
        public static void Send(Socket client, String data)
        {
            byte[] byteData = Encoding.ASCII.GetBytes(data);
            try
            {
                client.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallBack.SendCallback), client);
            }
            catch(Exception ex)
            {
                Client.GetExceptionHandle(ex.Message);
            }
            
        }
    }
}
