using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.NetworkMiddleware.NetworkData
{
    [Serializable]
    public class ReponseAllRequests
    {
        public string Code { get; set; }
        public string Status { get; set; }
        public string Reponse { get; set; }
    }
}
