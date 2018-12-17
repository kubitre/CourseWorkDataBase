using System;

namespace AdminPanel.Models.Payloads
{
    [Serializable]
    public class AuthPayload : Payload
    {
        public string StatusCode { get; set; }
        public string responseMessage { get; set; }
    }
}
