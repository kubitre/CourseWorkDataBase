using System.Net.Sockets;
using System.Text;

namespace ServerDb.ServerData
{
    public class StateObject
    {
        public Socket WorkSocket = null;
        public const int BufferSize = 10000;
        public byte[] Buffer = new byte[BufferSize];
        public StringBuilder MessageForReciveData = new StringBuilder();
    }
}
