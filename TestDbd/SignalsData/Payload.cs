using System;
using System.Collections.Generic;
using System.Text;

namespace ServerDb.SignalsData
{
    [Serializable]
    public class Payload
    {
        public int Count { get; set; }
        public int Offset { get; set; }
    }
}
