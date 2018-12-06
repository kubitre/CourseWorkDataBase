using System;
using System.Text;

namespace AdminPanel.NetworkMiddleware.HandlersForRequest
{
    class RecieveCallBack
    {
        public static void ReceiveCallback(IAsyncResult ar)
        {
            var response = "";

            try
            {  
                var state = (StateObject)ar.AsyncState;
                var client = state.WorkSocket;

                int bytesRead = client.EndReceive(ar);

                if (bytesRead > 0)
                {
                    state.MessageForRecieving.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));
  
                    client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                        new AsyncCallback(ReceiveCallback), state);
                }
                else
                {
                    if (state.MessageForRecieving.Length > 1)
                    {
                        response = state.MessageForRecieving.ToString();
                    }
                    Client.receiveDone.Set();

                    new HandlersAfterRequesting.HandleAuthentification().Handle(state);
               
                }
            }
            catch (Exception e)
            {
                Client.GetExceptionHandle(e.Message);
            }
        }
    }
}
