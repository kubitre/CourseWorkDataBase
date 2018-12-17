using ServerDb.ServerData;
using System;
using System.Text;

namespace ServerDb.HandlersForRequest
{
    class TestConnection : IHandler
    {
        public void hand(StateObject state)
        {
            var bytes = Encoding.UTF8.GetBytes("connected succes!");
            try
            {
                state.WorkSocket.BeginSend(bytes, 0, bytes.Length, 0, new AsyncCallback(ServerHandlers.SendCallBack.SendCallback), state.WorkSocket);

                ServerDb.Server.allDone.Set();
            }
            catch(Exception ex)
            {
                ServerDb.Server.NewExceptionRequest(ex.Message);
            }
            

        }
    }
}
