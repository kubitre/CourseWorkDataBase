using NetworkMiddleware.NetworkData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.NetworkMiddleware.NetworkData
{
    public class Dish
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Recipe> Products { get; set; }
        public double Outer { get; set; }
        public DateTime Date { get; set; }
        public string Cooperator { get; set; }
    }
}
