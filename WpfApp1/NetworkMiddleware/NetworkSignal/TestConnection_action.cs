using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace AdminPanel.NetworkMiddleware.NetworkSignal
{
    class TestConnection_action : ISignal
    {
        public const string ActionType = "testconnection";
        internal ManualResetEvent TimeOutActive = new ManualResetEvent(false);
        int timeOutFlag;
        System.Threading.Timer timer;
        bool isNormal = false;

        public bool Handle(params object [] param)
        {
            try
            {
                var remotePoint = Client.ipPoint;
                var socket = new Socket(remotePoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                IAsyncResult result = socket.BeginConnect(remotePoint, HandlersForRequest.ConnectionCallBack.ConnectCallback, socket);

                if (!result.IsCompleted)
                {
                    timer = new System.Threading.Timer(OnTimer, null, 1000, Timeout.Infinite);
                }
                TimeOutActive.WaitOne();

                if (!isNormal)
                    return false;
                Client.connectDone.WaitOne();

                HandlersForRequest.SendData.Send(socket, ActionType);
                Client.sendDone.WaitOne();

                HandlersForRequest.RecieveData.Receive(socket);
                Client.receiveDone.WaitOne();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        void OnTimer(object obj)
        {
            if (Interlocked.CompareExchange(ref timeOutFlag, 2, 0) != 0)
                isNormal = true;

            isNormal = false;
            TimeOutActive.Set();

        }
    }
}
