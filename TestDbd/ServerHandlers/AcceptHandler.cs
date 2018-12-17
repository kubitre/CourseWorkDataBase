﻿using ServerDb.ServerData;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ServerDb.ServerHandlers
{
    public static class AcceptHandler
    {
        public static ManualResetEvent allDone = ServerDb.Server.allDone;

        public static void AcceptCallback(IAsyncResult ar)
        {

            var listener = (Socket)ar.AsyncState;
            var handler = listener.EndAccept(ar);

                       
            Console.WriteLine($"[LOG {DateTime.Now}]: accept new client on adress: {handler.RemoteEndPoint}");

            var state = new StateObject();
            state.WorkSocket = handler;

            if (state.WorkSocket.Connected)
                handler.BeginReceive(state.Buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(RecieveDataAsync.ReadCallback), state);

            allDone.Set();
        }
    }
}
