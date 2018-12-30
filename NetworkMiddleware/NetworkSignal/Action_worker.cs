using AdminPanel.NetworkMiddleware.NetworkData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.NetworkMiddleware.NetworkSignal
{
    public class Action_worker
    {
        public bool HandleWorker(StateObject state, string Payload, string code)
        {
            var remotePoint = state.clientNetwork.ipPoint;

            if (!state.WorkSocket.Connected)
                state.WorkSocket = new Socket(remotePoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            //var socket = new Socket(remotePoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            //state.WorkSocket = socket;

            state.WorkSocket.BeginConnect(remotePoint, HandlersForRequest.ConnectionCallBack.ConnectCallback, state);
            state.clientNetwork.connectDone.WaitOne();

            var requestpacket = new NetworkData.RequestPacket() { Code = code, RequestBody = Payload};

            var message = Newtonsoft.Json.JsonConvert.SerializeObject(requestpacket);
            state.SetMessageSend(message);

            HandlersForRequest.SendData.Send(state);
            state.clientNetwork.sendDone.WaitOne();

            HandlersForRequest.RecieveData.Receive(state);
            state.clientNetwork.receiveDone.WaitOne();

            return CheckResponse(state.MessageForRecieving.ToString(), code);
        }

        public bool CheckResponse(string mess, string code)
        {
            try
            {
                var reponse = Newtonsoft.Json.JsonConvert.DeserializeObject<ReponseAllRequests>(mess);
                if (reponse.Code.Equals(code) & reponse.Status.Equals(NetworkResponseCodes.StatusCode.STATUS_SUCCESS))
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                //TODO: TRUCN THE EXCEPTION TO MAIN WINDOW!
                return false;
            }
        }

    }
}
