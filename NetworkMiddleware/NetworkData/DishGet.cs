using NetworkMiddleware.NetworkData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.NetworkMiddleware.NetworkData
{
    [Serializable]
    public class DishGet
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<string> Products { get; set; }
        public double Outer { get; set; }
    }
}
