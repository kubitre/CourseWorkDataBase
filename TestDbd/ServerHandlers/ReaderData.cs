using ServerDb.ServerData;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace ServerDb.ServerHandlers
{
    public class ReaderData
    {
        public static void ReadDataFromStream(Socket socket)
        {
            try
            {
                var state = new StateObject();
                state.WorkSocket = socket;

                state.WorkSocket.BeginReceive(state.Buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ServerHandlers.RecieveDataAsync.ReadCallback), socket);
            }
            catch(Exception ex)
            {
                ServerDb.Server.NewExceptionRequest(ex.Message);
            }
        }
    }
}
