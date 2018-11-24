using ServerDB.ServerData;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServerDB.ServerHandlers
{
    class RecieveDataAsync
    {
        public static void ReadCallback(IAsyncResult ar)
        {
            var content = String.Empty;
           
            var state = (StateObject)ar.AsyncState;
            var handler = state.WorkSocket;

            int bytesRead = handler.EndReceive(ar);

            if (bytesRead > 0)
            {
                Console.WriteLine($"[LOG {DateTime.Now}]: Recieved data on {bytesRead} bytes");

                state.MessageForReciveData.Append(Encoding.ASCII.GetString(
                    state.Buffer, 0, bytesRead));

                HandlersForRequest.HandlerRequestBuilder.RequestBuild(state);
            }
        }
    }
}
