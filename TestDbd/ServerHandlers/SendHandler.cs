using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace ServerDB.ServerHandlers
{
    class SendHandler
    {
        private static void Send(Socket handler, String data)
        { 
            byte[] byteData = Encoding.ASCII.GetBytes(data);

            Console.WriteLine($"[LOG {DateTime.Now}]: start send data to client: {data}");

            handler.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallBack.SendCallback), handler);
        }
    }
}
