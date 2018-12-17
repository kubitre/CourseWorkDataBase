using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace ServerDb.ServerHandlers
{
    class SendHandler
    {
        public static void Send(Socket handler, String data)
        {
            var encoder = Encoding.UTF8;
            byte[] byteData = encoder.GetBytes(data);

            Console.WriteLine($"[LOG {DateTime.Now}]: start send data to client: {data}");

            handler.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallBack.SendCallback), handler);
        }
    }
}
