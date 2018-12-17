using DatabaseMiddlware.ModelForTransmition;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServerDb.ModelForTransmition
{
    [Serializable]
    public class Dish
    {
        public string Action { get; set; }
        public DishBlock Payload { get; set; }
    }
}
