using System.Net.Sockets;
using System.Text;

namespace ServerDB.ServerData
{
    public class StateObject
    {
        public Socket WorkSocket = null;
        public const int BufferSize = 1024;
        public byte[] Buffer = new byte[BufferSize];
        public StringBuilder MessageForReciveData = new StringBuilder();
    }
}
