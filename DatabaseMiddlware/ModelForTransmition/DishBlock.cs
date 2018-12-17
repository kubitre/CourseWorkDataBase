using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseMiddlware.ModelForTransmition
{
    [Serializable]
    public class DishBlock
    {
        public int countDishes { get; set; }
        public int offsetAboutFirst { get; set; }
    }
}
