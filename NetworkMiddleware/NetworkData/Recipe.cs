using AdminPanel.NetworkMiddleware.NetworkData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkMiddleware.NetworkData
{
    public class Recipe
    {
        public Guid Id { get; set; }
        public Dish Dish { get; set; }
        public Guid IdProduct { get; set; }
        public double Amount { get; set; }
    }
}
