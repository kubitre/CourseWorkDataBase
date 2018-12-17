using System;

namespace ServerDb.NetworkData
{
    [Serializable]
    public class ReponseAllRequests
    {
        public string Code { get; set; }
        public string Status { get; set; }
        public string Reponse { get; set; }
    }
}
