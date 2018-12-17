using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.NetworkMiddleware.NetworkData
{
    [Serializable]
    public class Position
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
