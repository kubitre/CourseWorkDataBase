using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.NetworkMiddleware.NetworkData
{
    [Serializable]
    public class RequestPacket
    {
        public string Code { get; set; }
        public string RequestBody { get; set; }
    }
}
