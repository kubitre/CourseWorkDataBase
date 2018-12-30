using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.NetworkMiddleware.NetworkData
{
    [Serializable]
    public class WorkBook
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string Position { get; set; }
        public int OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public string Reason { get; set; }
    }
}
