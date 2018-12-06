using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace ServerDb
{
    class Server
    {
        private static int _port = 10234;
        private Socket TcpChannel { get; set; }
        public static ManualResetEvent allDone = new ManualResetEvent(false);
        public static ManualResetEvent ExceptOut = new ManualResetEvent(false);


        internal delegate void ExceptionHandle(string code);
        public static event ExceptionHandle NewException;


        public void Run()
        {
            NewException += HandleExceptionNew;
            var worker = new Thread(new ThreadStart(Worker));
            worker.Start();
        }

        private void Worker()
        {
            IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = ipHost.AddressList[1];
            IPEndPoint sock = new IPEndPoint(ipAddress, _port);

            this.TcpChannel = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                this.TcpChannel.Bind(sock);
                this.TcpChannel.Listen(100);
                Console.WriteLine($"[LOG {DateTime.Now}]: Server is started on {sock.Address}:{sock.Port}");
                Console.WriteLine($"[LOG {DateTime.Now}]: Start listening connection for this server!");

                while (true)
                {
                    allDone.Reset();


                    Console.WriteLine($"[LOG {DateTime.Now}]: Waiting connection for this server");

                    this.TcpChannel.BeginAccept(new AsyncCallback(ServerDB.ServerHandlers.AcceptHandler.AcceptCallback), this.TcpChannel);

                    allDone.WaitOne();
                }
            }
            catch( Exception ex)
            {
                Console.WriteLine($"[LOG {DateTime.Now}]: Error bind of local enppoint. Code error: {ex.Message}");
            }
        }

        public static void NewExceptionRequest(string code)
        {
            NewException(code);
        }

        private void HandleExceptionNew(string code)
        {
            Console.WriteLine($"[LOG {DateTime.Now}]: New Exception handle! exception code: {code}");
        }

        //private void PrepareRequest(Socket socket, string message)
        //{
        //    var regLogin = new Regex("(login:{[a-zA-Z]{4,}[0-9]{2,}})");
        //    var regPassword = new Regex("(password:{[a-zA-Z0-9]{6,30}})");

        //    Console.WriteLine("Check Login and Password");

        //    if (message.Contains("login") & message.Contains("password"))
        //    {
        //        Console.WriteLine("message contain login and password");
        //        var mathcesLogin = regLogin.Matches(message);
        //        var mathcersPassword = regPassword.Matches(message);

        //        if(mathcesLogin.Count == 1 & mathcersPassword.Count == 1)
        //        {

        //            Console.WriteLine("Data is available!");
        //            var regLoginValue = new Regex("[a-zA-Z]{4,}[0-9]{2,}}");
        //            var regPasswordValue = new Regex(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$");

        //            if(regLoginValue.Matches(mathcesLogin[0].Value).Count == 1 & regPasswordValue.Matches(mathcersPassword[0].Value).Count == 1)
        //            {
        //                Console.WriteLine("Work with db!");
        //                WorkWithAuthentificate(socket, regLoginValue.Matches(mathcesLogin[0].Value)[0].Value, regPasswordValue.Matches(mathcersPassword[0].Value)[0].Value);
        //            }
        //        }
        //        else
        //        {
        //            Console.WriteLine("Data is not available!");
        //        }
        //    }
        //    else
        //    {
        //        Console.WriteLine("message not contain login and password!");
        //        socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                
        //    }
        //}

        //private void WorkWithAuthentificate(Socket socket, string username, string password)
        //{
        //    Console.WriteLine("Authentificate processing");

        //    //var sendMessage = new AuthentificateSignal();

        //    //if (DatabaseMiddlware.Workers.Users.UserHandler.Authentificate(username, password))
        //    //    sendMessage.Setting("login_succes");
        //    //else
        //    //    sendMessage.Setting("login_error");



        //    //var messgeByte = Encoding.UTF8.GetBytes(Json.JsonParser.Serialize(sendMessage));

        //    //socket.Send(messgeByte);
        //    //ShutDown(socket);
        //}


        //private void ShutDownSocket(Socket socket)
        //{
        //    socket.Shutdown(SocketShutdown.Both);
        //    socket.Close();
        //}

    }
}
