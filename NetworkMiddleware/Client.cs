using AdminPanel.NetworkMiddleware.NetworkSignal;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace AdminPanel.NetworkMiddleware
{
    public class Client
    {
        private static int _port = 10234;
        internal IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse("192.168.100.3"), _port);

        private string ApplicationType;

        public string Response;

        //Signals for thread worker
        internal ManualResetEvent connectDone =
        new ManualResetEvent(false);
        internal ManualResetEvent sendDone =
            new ManualResetEvent(false);
        internal ManualResetEvent receiveDone =
            new ManualResetEvent(false);

        public delegate void AcceptException(string message);
        public event AcceptException GetExceptionOutput;

        private Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        public delegate void Attention(string mess);
        public event Attention NewMess;

        public StateObject State;

        public Client(
            string appType
            )
        {
            this.ApplicationType = appType;
        }

        public Client()
        {
            this.ApplicationType = "AdminPanel";
        }


        public bool RequestHandle(string nameHandler, params object[] param)
        {
            if(State == null)
                State = new StateObject();

            State.clientNetwork = this;
            State.WorkSocket = socket;

            switch (nameHandler)
            {
                case TestConnection_action.ActionType:
                    if (new TestConnection_action().Handle(State))
                        return true;
                    return false;
                case Authentification_action.ActionTypeGet:
                    if (new Authentification_action().Handle(State, param))
                        return true;
                    break;
                case Dish_action.ActionTypeGet:
                    if (new Dish_action().Handle(State, param))
                        return true;
                    break;
                case Dish_action.ActionTypeCreate:
                    if (new Dish_action().HandleSet(State, param))
                        return true;
                    break;
                case Menu_action.ActionTypeGet:
                    if (new Menu_action().Handle(State, param))
                        return true;
                    break;
                case Menu_action.ActionTypeCreate:
                    if (new Menu_action().HandleSet(State, param))
                        return true;
                    break;
                case Street_Action.ActionTypeGet:
                    if (new Street_Action().Handle(State, param))
                        return true;
                    break;
                case Street_Action.ActionTypeCreate:
                    if (new Street_Action().HandleSet(State, param))
                        return true;
                    break;
                case Cooperator_action.ActionTypeGet:
                    if (new Cooperator_action().Handle(State, param))
                        return true;
                    break;
                case Cooperator_action.ActionTypeCreate:
                    if (new Cooperator_action().HandleSet(State, param))
                        return true;
                    break;
                case User_action.ActionTypeGet:
                    if (new User_action().Handle(State, param))
                        return true;
                    break;
                case User_action.ActionTypeCreate:
                    if (new User_action().HandleSet(State, param))
                        return true;
                    break;
                case Product_action.ActionTypeGet:
                    if (new Product_action().Handle(State, param))
                        return true;
                    break;
                case Product_action.ActionTypeCreate:
                    if (new Product_action().HandleSet(State, param))
                        return true;
                    break;
                case Position_action.ActionTypeGet:
                    if (new Position_action().Handle(State, param))
                        return true;
                    break;
                case Position_action.ActionTypeCreate:
                    if (new Position_action().HandleSet(State, param))
                        return true;
                    break;
                case Category_action.ActionTypeGet:
                    if (new Category_action().Handle(State, param))
                        return true;
                    break;
                case Category_action.ActionTypeCreate:
                    if (new Category_action().HandleSet(State, param))
                        return true;
                    break;
                default:
                    //TODO: TRUNC Exception
                    break;
            }

            return false;
        }

        public void GetExceptionHandle(string message) => GetExceptionOutput(message);

        public void CloseSocket() => this.State.WorkSocket.Close();
    }
}
