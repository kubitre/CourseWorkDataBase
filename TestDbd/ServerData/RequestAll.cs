using System;
using System.Collections.Generic;
using System.Text;

namespace ServerDb.ServerData
{
    [Serializable]
    public class RequestAll
    {
        public string Code { get; set; }
        public string RequestBody { get; set; }
    }
}
