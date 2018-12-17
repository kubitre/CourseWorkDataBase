using System;
using System.Net.Sockets;
using System.Text;

namespace AdminPanel.NetworkMiddleware
{
    public class StateObject
    {
        public Socket WorkSocket;
        public const int BufferSize = 29980;
        public byte[] buffer = new byte[BufferSize];
        public StringBuilder MessageForRecieving = new StringBuilder();
        public Client clientNetwork;
        private Encoding encoder = Encoding.UTF8;

        public string LastResponse {
            get {
                try
                {
                    var ResponseHandled = Newtonsoft.Json.JsonConvert.DeserializeObject<NetworkData.ReponseAllRequests>(this.MessageForRecieving.ToString());

                    return ResponseHandled.Reponse;
                }
                catch (Exception e)
                {
                    return null;
                }
            }
        }

        public void SetMessageSend(string message)
        {
            this.buffer = null;
            this.buffer = encoder.GetBytes(message);
        }

        public void TranslitReceivedDataFomWindowsCoding() => this.MessageForRecieving.Append(encoder.GetString(this.buffer));
        public void CleanBuffer() => this.buffer = new byte[BufferSize];
        
    }
}
