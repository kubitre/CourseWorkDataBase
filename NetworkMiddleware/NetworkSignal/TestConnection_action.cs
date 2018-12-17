using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace AdminPanel.NetworkMiddleware.NetworkSignal
{
    public class TestConnection_action : ISignal
    {
        public const string ActionType = "testconnection";
        internal ManualResetEvent TimeOutActive = new ManualResetEvent(false);
        int timeOutFlag;
        System.Threading.Timer timer;


        public bool Handle(StateObject state, params object [] param)
        {
            try
            {
                var remotePoint = state.clientNetwork.ipPoint;
                var socket = new Socket(remotePoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                IAsyncResult result = socket.BeginConnect(remotePoint, HandlersForRequest.ConnectionCallBack.ConnectCallback, state);

                if (!result.IsCompleted)
                {
                    timer = new System.Threading.Timer(OnTimer, null, 2000, Timeout.Infinite);
                }
                TimeOutActive.WaitOne();

                if (!result.IsCompleted)
                {
                    return false;
                }

                try
                {
                    var Payload = new NetworkData.RequestPacket()
                    {
                        Code = NetworkResponseCodes.TestconnectionCodes.TESTCONNECTION_GET_CODE,
                        RequestBody = "test connection"
                    };

                    var messagePayload = Newtonsoft.Json.JsonConvert.SerializeObject(Payload);

                    return new Action_worker().HandleWorker(state, messagePayload, NetworkResponseCodes.TestconnectionCodes.TESTCONNECTION_GET_CODE);
                   
                }
                catch(Exception ex)
                {
                    //var Payload = new NetworkData.RequestPacket()
                    //{
                    //    Code = NetworkResponseCodes.TestconnectionCodes.TESTCONNECTION_GET_CODE,
                    //    RequestBody = "test connection"
                    //};


                    //var messagePayload = Newtonsoft.Json.JsonConvert.SerializeObject(Payload);

                    //return new Action_worker().HandleWorker(state, messagePayload, NetworkResponseCodes.TestconnectionCodes.TESTCONNECTION_GET_CODE);
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public bool HandleDelete(StateObject state, params object[] param)
        {
            throw new NotImplementedException();
        }

        public bool HandleSet(StateObject state, params object[] param)
        {
            throw new NotImplementedException();
        }

        public bool HandleUpdate(StateObject state, params object[] param)
        {
            throw new NotImplementedException();
        }

        void OnTimer(object obj)
        {
            if (Interlocked.CompareExchange(ref timeOutFlag, 2, 0) != 0)
                ;

            TimeOutActive.Set();

        }
    }
}
