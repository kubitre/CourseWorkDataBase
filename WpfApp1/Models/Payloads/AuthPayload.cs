using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.Models.Payloads
{
    [Serializable]
    public class AuthPayload : Payload
    {
        public string StatusCode { get; set; }
        public string responseMessage { get; set; }
    }
}
