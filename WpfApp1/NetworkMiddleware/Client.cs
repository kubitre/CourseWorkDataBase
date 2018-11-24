using AdminPanel.NetworkMiddleware.NetworkSignal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AdminPanel.NetworkMiddleware
{
    public static class Client
    {
        private static int _port = 10234;
        internal static IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse("192.168.100.3"), _port);

        //Signals for thread worker
        internal static ManualResetEvent connectDone =
        new ManualResetEvent(false);
        internal static ManualResetEvent sendDone =
            new ManualResetEvent(false);
        internal static ManualResetEvent receiveDone =
            new ManualResetEvent(false);

        private static Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        public delegate void Attention(string mess);
        public static event Attention NewMess;


        public static bool RequestHandle(string nameHandler)
        {
            switch (nameHandler)
            {
                case TestConnection_action.ActionType:
                    if (new TestConnection_action().Handle())
                        return true;
                    return false;
                case Authentification_action.ActionType:
                    if (new Authentification_action().Handle())
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

        //public static bool TestConnection()
        //{
        //    try

        //    {
        //        socket.Connect(ipPoint);
        //        var testMess = Encoding.UTF8.GetBytes("This is test connection for server!\n");
        //        socket.Send(testMess);

        //        var data = new byte[256];
        //        int bytes = 0;

        //        var builder = new StringBuilder();

        //        do
        //        {
        //            bytes = socket.Receive(data);
        //            builder.Append(Encoding.UTF8.GetString(data, 0, bytes));

        //        }
        //        while (socket.Available > 0);

        //        //NewMess(builder.ToString());

        //        socket.Shutdown(SocketShutdown.Both);
        //        socket.Close();
        //        return true;
        //    }
        //    catch(Exception ex)
        //    {
        //        return false;
        //    }
            
        //}

        //public static bool SendAuthentification(string username, string password)
        //{
        //    try
        //    {
        //        var sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        //        sock.Connect(ipPoint);
        //        var mess = Encoding.UTF8.GetBytes($@"login:{{{username}}}, password:{{{password}}}");

        //        sock.Send(mess);

        //        var data = new byte[256];
        //        int bytes = 0;

        //        var builder = new StringBuilder();

        //        do
        //        {
        //            bytes = sock.Receive(data);
        //            builder.Append(Encoding.UTF8.GetString(data, 0, bytes));
        //        }
        //        while (sock.Available > 0);

        //        sock.Shutdown(SocketShutdown.Both);
        //        sock.Close();

        //        if (builder.ToString().Equals("login_succes"))
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    catch(Exception ex)
        //    {
        //        return false;
        //    }
        //}

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
