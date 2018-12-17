using ServerDb.ServerData;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace ServerDb.HandlersForRequest
{
    class NullHandler : IHandler
    {
        public void hand(StateObject state)
        {
            var bytes = Encoding.UTF8.GetBytes("error handle!");
            state.WorkSocket.BeginSend(bytes, 0, bytes.Length, 0, new AsyncCallback(ServerHandlers.SendCallBack.SendCallback), state.WorkSocket);

            ServerDb.Server.allDone.Set();
        }
    }
}
