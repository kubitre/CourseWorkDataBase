using AdminPanel.NetworkMiddleware.NetworkSignal;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace AdminPanel.NetworkMiddleware
{
    public static class Client
    {
        private static int _port = 10234;
        internal static IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), _port);

        //Signals for thread worker
        internal static ManualResetEvent connectDone =
        new ManualResetEvent(false);
        internal static ManualResetEvent sendDone =
            new ManualResetEvent(false);
        internal static ManualResetEvent receiveDone =
            new ManualResetEvent(false);

        public delegate void AcceptException(string message);
        public static event AcceptException GetExceptionOutput;

        private static Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        public delegate void Attention(string mess);
        public static event Attention NewMess;


        public static bool RequestHandle(string nameHandler, params object[] param)
        {
            switch (nameHandler)
            {
                case TestConnection_action.ActionType:
                    if (new TestConnection_action().Handle())
                        return true;
                    return false;
                case Authentification_action.ActionType:
                    if (new Authentification_action().Handle(param))
                        return true;
                    return false;
            }

            return false;
        }

        public static void Run()
        {
            var worker = new Thread(new ThreadStart(Worker));
            worker.Start();
        }

        public static void GetExceptionHandle(string message) => GetExceptionOutput(message);

        private static void Worker()
        {
            try
            {
                

            }
            catch(Exception ex)
            {

            }
            
            
        }
    }
}
