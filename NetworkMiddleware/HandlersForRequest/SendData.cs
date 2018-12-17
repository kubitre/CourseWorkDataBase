using System;
using System.Net.Sockets;
using System.Text;

namespace AdminPanel.NetworkMiddleware.HandlersForRequest
{
    class SendData
    {
        public static void Send(StateObject state)
        {
            try
            {
                state.WorkSocket.BeginSend(state.buffer, 0, state.buffer.Length, 0,
                new AsyncCallback(SendCallBack.SendCallback), state);
            }
            catch(Exception ex)
            {
                state.clientNetwork.GetExceptionHandle(ex.Message);
            }
            
        }
    }
}
