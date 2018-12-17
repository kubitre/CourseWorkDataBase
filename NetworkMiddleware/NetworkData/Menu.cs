using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.NetworkMiddleware.NetworkData
{
    public class Menu
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<string> Dishes { get; set; }
        public double Outer { get; set; }
        public string Coocker { get; set; }
        public DateTime Date { get; set; }
    }
}
