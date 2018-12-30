using System;

namespace AdminPanel.Models
{
    [Serializable]
    public class Payload
    {
        public int Count { get; set; }
        public int Offset { get; set; }
    }
}
