using System;
using System.Net.Sockets;

namespace AdminPanel.NetworkMiddleware.NetworkSignal
{
    public class Authentification_action : ISignal
    {
        public const string ActionType = "authentification";
        public string Payload = "";

        public bool Handle(params object[] param)
        {
            try
            {
                var remotePoint = Client.ipPoint;
                var socket = new Socket(remotePoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                if (param.Length != 2)
                    throw new ArgumentException("autentification payload must be contain username and pasword!");

                Payload = $"\"username\":\"{(string)param[0]}\", \"password\":\"{(string)param[1]}\"";

                socket.BeginConnect(remotePoint, HandlersForRequest.ConnectionCallBack.ConnectCallback, socket);
                Client.connectDone.WaitOne();

                HandlersForRequest.SendData.Send(socket, ActionType + $", payload:{Json.JsonParser.Serialize(Payload)}");
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
    }
}
