using System;
using System.Net.Sockets;

namespace AdminPanel.NetworkMiddleware.HandlersForRequest
{
    class RecieveData
    {
        public static void Receive(StateObject state)
        {
            try
            {
                state.CleanBuffer();
                state.WorkSocket.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                    new AsyncCallback(RecieveCallBack.ReceiveCallback), state);
            }
            catch (Exception e)
            {
                state.clientNetwork.GetExceptionHandle(e.Message);
            }
        }
    }
}
