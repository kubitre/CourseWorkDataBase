using System;
using System.Text;

namespace AdminPanel.NetworkMiddleware.HandlersForRequest
{
    public class RecieveCallBack
    {
        public static void ReceiveCallback(IAsyncResult ar)
        {
            try
            {  
                var state = (StateObject)ar.AsyncState;
                var client = state.WorkSocket;

                int bytesRead = client.EndReceive(ar);

                if (bytesRead > 0)
                {
                    state.TranslitReceivedDataFomWindowsCoding();
  
                    client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                        new AsyncCallback(ReceiveCallback), state);
                }
                else
                {
                    if (state.MessageForRecieving.Length > 1)
                    {
                        (ar.AsyncState as StateObject).clientNetwork.Response = state.MessageForRecieving.ToString();
                    }
                    (ar.AsyncState as StateObject).clientNetwork.receiveDone.Set();
                }
            }
            catch (Exception e)
            {
                (ar.AsyncState as StateObject).clientNetwork.GetExceptionHandle(e.Message);
            }
        }
    }
}
