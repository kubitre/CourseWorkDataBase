using System;
using System.Net.Sockets;
using Newtonsoft.Json;
using AdminPanel.Models;

namespace AdminPanel.NetworkMiddleware.NetworkSignal
{
    [Serializable]
    public class Authentification_action : ISignal
    {
        public const string ActionType = "authentification";

        public string Action = ActionType;
        public AuthBLock Payload;

        public bool Handle(params object[] param)
        {
            try
            {
                var remotePoint = Client.ipPoint;
                var socket = new Socket(remotePoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                if (param.Length != 2)
                    throw new ArgumentException("autentification payload must be contain username and pasword!");

                Payload = new AuthBLock { Username = (string)param[0], Password = (string)param[1] };

                socket.BeginConnect(remotePoint, HandlersForRequest.ConnectionCallBack.ConnectCallback, socket);
                Client.connectDone.WaitOne();

                var messageForSend = JsonConvert.SerializeObject(this);

                HandlersForRequest.SendData.Send(socket, messageForSend);
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
