using ServerDb.ServerData;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServerDb.ServerHandlers
{
    class RecieveDataAsync
    {
        public static void ReadCallback(IAsyncResult ar)
        {
            try
            {
                var content = String.Empty;

                var state = (StateObject)ar.AsyncState;
                var handler = state.WorkSocket;

                int bytesRead = handler.EndReceive(ar);

                if (bytesRead > 0)
                {
                    Console.WriteLine($"[LOG {DateTime.Now}]: Recieved data on {bytesRead} bytes");

                    state.MessageForReciveData.Append(Encoding.UTF8.GetString(
                        state.Buffer, 0, bytesRead));

                    Console.WriteLine($"[LOG {DateTime.Now}]: Recieved message: {state.MessageForReciveData.ToString()}");

                    //(HandlersForRequest.HandlerRequestBuilder.RequestBuild(state) as HandlersForRequest.IHandler).hand(state);
                    HandlersForRequest.HandlerRequestBuilder.RequestBuild(state)(state.MessageForReciveData.ToString(), state);
                }
                else
                {
                    Console.WriteLine($"[LOG {DateTime.Now}]: Recieve message: {state.MessageForReciveData.ToString()}");
                    //(HandlersForRequest.HandlerRequestBuilder.RequestBuild(state) as HandlersForRequest.IHandler).hand(state);
                    HandlersForRequest.HandlerRequestBuilder.RequestBuild(state)(state.MessageForReciveData.ToString(), state);
                }
            }
                catch(Exception ex)
            {
                ServerDb.Server.NewExceptionRequest(ex.Message);
            }
           
        }
    }
}
