using System.Net.Sockets;
using System.Text;

namespace AdminPanel.NetworkMiddlware
{
    class StateObject
    {
        public Socket WorkSocket;
        public const int BufferSize = 256;
        public byte[] buffer = new byte[BufferSize];
        public StringBuilder MessageForRecieving = new StringBuilder();
    }
}
